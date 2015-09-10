using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using BusinessObjects;

namespace DataObjects.EntityFramework
{
    public class OrderDao:IOrderDao
    {
        static OrderDao()
        {
            Mapper.CreateMap<Order, OrderBO>();
            Mapper.CreateMap<OrderBO, Order>();
            Mapper.CreateMap<LibOrderStatu, OrderStatusBO>();
            Mapper.CreateMap<OrderStatusBO, LibOrderStatu>();
            Mapper.CreateMap<LibDepositType, DepositMethodBO>();
            Mapper.CreateMap<DepositMethodBO, LibDepositType>();
            Mapper.CreateMap<ShippingMethodBO, LibShippingMethod>();
            Mapper.CreateMap<LibShippingMethod, ShippingMethodBO>();
            Mapper.CreateMap<Customer, CustomerBO>();
            Mapper.CreateMap<CustomerBO, Customer>();
            Mapper.CreateMap<User, MemberBO>();
            Mapper.CreateMap<MemberBO, User>();
            Mapper.CreateMap<OrderItem, OrderItemlBO>();
            Mapper.CreateMap<OrderItemlBO, OrderItem>();
        }

        #region Order

        public OrderBO GetOrderById(int orderId)
        {
            using (var context = new InThuDoEntities())
            {
                var order = (from o in context.Orders
                             where o.OrderId == orderId
                             && (o.Deleted == false || o.Deleted == null)
                             select new OrderBO()
                             {
                                 OrderId = o.OrderId,
                                 Deposit = o.Deposit,
                                 DepositTypeId = o.DepositTypeId,
                                 ShippingMethodId = o.ShippingMethodId,
                                 OrderDate = o.OrderDate,
                                 CustomerId = o.CustomerId,
                                 BusinessManId = o.UserId,
                                 CreatedBy = o.CreatedBy,
                                 CreatedDate = o.CreatedDate,
                                 LastEditedBy = o.LastEditedBy,
                                 LastEditedDate = o.LastEditedDate,
                                 Deleted = o.Deleted,
                                 ExpectedCompleteDate = o.ExpectedCompleteDate,
                                 Note = o.Note,
                                 DeliveryAddress = o.DeliveryAddress,
                                 ApprovedByCustomer = o.ApprovedByCustomer,
                                 ApprovedDate = o.ApprovedDate,
                                 VAT = o.VAT,
                             }).FirstOrDefault();

                if (order != null)
                {
                    order.OrderTotal = GetOrderTotal(order);
                    order.OrderStatus = this.GetOrderStatus(order.OrderId);
                }
                return order;
            }
        }

        public OrderBO InsertOrderInfo(OrderBO order)
        {
            using (var context = new InThuDoEntities())
            {
                Order entity = new Order() { 
                    Deposit = order.Deposit,
                    DepositTypeId = order.DepositTypeId,
                    ShippingMethodId = order.ShippingMethodId,
                    OrderDate = order.OrderDate,
                    CustomerId = order.CustomerId,
                    UserId = order.BusinessManId,
                    CreatedBy = order.CreatedBy,
                    CreatedDate = order.CreatedDate,
                    LastEditedBy = order.LastEditedBy,
                    LastEditedDate = order.LastEditedDate,
                    ExpectedCompleteDate = order.ExpectedCompleteDate,
                    Note = order.Note,
                    DeliveryAddress = order.DeliveryAddress,
                    ApprovedByCustomer = order.ApprovedByCustomer,
                    ApprovedDate = order.ApprovedDate,
                    VAT = order.VAT
                };
                context.Orders.Add(entity);
                context.Entry(entity).State = System.Data.EntityState.Added;
                context.SaveChanges();

                order.OrderId = entity.OrderId;
                return order;
            }
        }

        public void UpdateOrderInfo(OrderBO order)
        {
            using (var context = new InThuDoEntities())
            {
                Order entity = context.Orders.Where(o => o.OrderId == order.OrderId).FirstOrDefault();
                entity.OrderDate = order.OrderDate;
                entity.Deposit = order.Deposit;
                entity.DepositTypeId = order.DepositTypeId;
                entity.ShippingMethodId = order.ShippingMethodId;
                entity.CustomerId = order.CustomerId;
                entity.UserId = order.BusinessManId;
                entity.LastEditedBy = order.LastEditedBy;
                entity.LastEditedDate = order.LastEditedDate;
                entity.ExpectedCompleteDate = order.ExpectedCompleteDate;
                entity.Note = order.Note;
                entity.DeliveryAddress = order.DeliveryAddress;
                entity.ApprovedByCustomer = order.ApprovedByCustomer;
                entity.ApprovedDate = order.ApprovedDate;
                entity.VAT = order.VAT;
                entity.Note = order.Note;

                context.Entry(entity).State = System.Data.EntityState.Modified;
                context.SaveChanges();
            }


        }

        public List<OrderBO> GetOrders(OrderSearch orderSearchObj)
        {
            using (var context = new InThuDoEntities())
            {
                var query = (from o in context.Orders
                             join oitem in context.OrderItems
                             on o.OrderId equals oitem.OrderId into orderItemGrop                           
                             from oitem2 in orderItemGrop.DefaultIfEmpty()
                             join designRequest in context.DesignRequests on oitem2.OrderItemId equals designRequest.OrderItemId into dsGroup
                             from designRequest2 in dsGroup.DefaultIfEmpty()
                             join or in context.UserOrganizationMapppings on o.UserId equals or.UserId
                             //join oStatusMapping in context.OrderStatusMappings on oitem2.OrderItemId equals oStatusMapping.OrderItemId into oStatusMappingGroup
                             //from oStatusMapping2 in oStatusMappingGroup.DefaultIfEmpty()
                             where
                             (oitem2.Deleted == false || oitem2.Deleted == null) &&
                             (orderSearchObj.OrderId == 0 || o.OrderId == orderSearchObj.OrderId) &&
                             (orderSearchObj.CustId == 0 || o.CustomerId == orderSearchObj.CustId) &&
                             (orderSearchObj.ProductId == 0 || oitem2.ProductId == orderSearchObj.ProductId) &&
                             (orderSearchObj.ShipMethodId == 0 || o.ShippingMethodId == orderSearchObj.ShipMethodId) &&
                             (orderSearchObj.DepositMethodId == 0 || o.DepositTypeId == orderSearchObj.DepositMethodId) &&
                                 //(orderSearchObj.OrderStatusId == 0 || oStatusMapping2.OrderStatusId == orderSearchObj.OrderStatusId)&&
                             (orderSearchObj.BusManId == 0 || o.UserId == orderSearchObj.BusManId) &&
                             (orderSearchObj.DesignerManId == 0 || designRequest2.DesignerId == orderSearchObj.DesignerManId) &&
                             (orderSearchObj.OrderFrom == null || o.OrderDate >= orderSearchObj.OrderFrom) &&
                             (orderSearchObj.OrderTo == null || o.OrderDate <= orderSearchObj.OrderTo) &&
                             (orderSearchObj.OrganizationId == 0 || or.OrganizationId == orderSearchObj.OrganizationId) &&       
                             (orderSearchObj.PrintingTypeIds.Count ==0 || orderSearchObj.PrintingTypeIds.Contains(oitem2.PrintingTypeId))&&
                             (o.Deleted == null || o.Deleted == false)
                             select new OrderBO
                             {
                                 OrderId = o.OrderId,
                                 OrderDate = o.OrderDate,
                                 Deposit = o.Deposit,
                                 DepositTypeId = o.DepositTypeId,
                                 ShippingMethodId = o.ShippingMethodId,
                                 CustomerId = o.CustomerId,
                                 BusinessManId = o.UserId,
                                 CreatedBy = o.CreatedBy,
                                 CreatedDate = o.CreatedDate,
                                 LastEditedBy = o.LastEditedBy,
                                 LastEditedDate = o.LastEditedDate,
                                 CustomerName = o.Customer.Name,
                                 BusinessManName = o.User.FullName,
                                 DepositTypeName = o.LibDepositType.Name,
                                 ShippingMethodName = o.LibShippingMethod.Name,
                                 ExpectedCompleteDate = o.ExpectedCompleteDate,
                                 Note = o.Note,
                                 DeliveryAddress = o.DeliveryAddress
                             }).Distinct().ToList();

                

                foreach (OrderBO o in query)
                {
                    o.OrderStatus = this.GetOrderStatus(o.OrderId);
                    o.OrderTotal = this.GetOrderTotal(o);

                    var orderItems = (from od in context.OrderItems
                                      where
                                      (od.OrderId == o.OrderId) && (od.Deleted == null || od.Deleted == false)
                                      select new OrderItemlBO()
                                      {
                                          OrderItemId = od.OrderItemId,
                                          OrderId = od.OrderId,
                                          Quantity= od.Quantity,
                                          Price = od.Price,
                                          PrintingTypeId = od.PrintingTypeId
                                      }).ToList();
                    if (orderItems.Count > 0)
                    {
                        foreach (OrderItemlBO od in orderItems)
                        {
                            od.OrderItemStatus = this.GetOrderItemStatusIncludedOverDue(od.OrderItemId);
                        }

                        o.OrderItems = orderItems.Distinct().ToList();
                    }
                }

                if (orderSearchObj.OrderDetailStatus != 0)
                {
                    var orderItems = (from od in context.OrderItems
                                      where
                                       (od.Deleted == null || od.Deleted == false)

                                      select new OrderItemlBO()
                                      {
                                          OrderItemId = od.OrderItemId,
                                          OrderId = od.OrderId,
                                          Quantity = od.Quantity,
                                          Price = od.Price
                                      }).ToList();
                    if (orderItems.Count > 0)
                    {
                        foreach (OrderItemlBO od in orderItems)
                        {
                            od.OrderItemStatus = this.GetOrderItemStatusIncludedOverDue(od.OrderItemId);
                        }
                    }

                    query = (from q in query
                             join od in orderItems on q.OrderId equals od.OrderId
                             where od.OrderItemStatus == orderSearchObj.OrderDetailStatus
                             select q).Distinct().ToList();
                }

                if (orderSearchObj.OrderStatus.Count >0)
                {
                    query = (from q in query
                             where orderSearchObj.OrderStatus.Contains(q.OrderStatus)
                             select q).Distinct().ToList();
                }

                return query;
            }
        }

        public void MarkOrderAsDeleted(int orderId)
        {
            using (var context = new InThuDoEntities())
            {
                Order order = context.Orders.Where(o => o.OrderId == orderId).FirstOrDefault();
                if (order == null) return;
                order.Deleted = true;
                context.SaveChanges();
            }
        }

        

        #endregion Order      

        #region Order Item

        public OrderItemlBO GetOrderDetailById(int id)
        {
            using (var context = new InThuDoEntities())
            {
                var query = (from od in context.OrderItems
                                   where od.OrderItemId == id &&
                                   (od.Deleted == null || od.Deleted == false)
                                   select od).FirstOrDefault();
                OrderItemlBO orderDetail = this.MapOrderDetail(query);

                if (orderDetail != null)
                {
                    orderDetail.OrderItemStatus = this.GetOrderItemStatusIncludedOverDue(orderDetail.OrderItemId);
                }
                return orderDetail;
            }
        }

        public List<OrderItemlBO> GetOrderDetailsByOrderId(int orderId)
        {
            using (var context = new InThuDoEntities())
            {
                var query = from od in context.OrderItems
                        where
                        (od.OrderId == orderId) &&
                        (od.Deleted == null || od.Deleted == false)
                        select od;
                List<OrderItemlBO> orderDetails = this.MapOrderDetailList(query);

                foreach (OrderItemlBO od in orderDetails)
                {
                    od.OrderItemStatus = this.GetOrderItemStatusIncludedOverDue(od.OrderItemId);
                }

                return orderDetails;
            }
        }

        public void UpdateOrderDetail(OrderItemlBO orderDetail)
        {
            using (var context = new InThuDoEntities())
            {
                var entity = context.OrderItems.Where(od => od.OrderItemId == orderDetail.OrderItemId).FirstOrDefault();
                if (entity == null) return;
                entity.ProductId = orderDetail.ProductId;
                entity.Specification = orderDetail.Specification;
                entity.Quantity = orderDetail.Quantity;
                entity.Price = orderDetail.Price;
                entity.LastEditedBy = orderDetail.LastEditedBy;
                entity.LastEditedOn = orderDetail.LastEditedOn;
                entity.IsCustomerHasDesign = orderDetail.IsCustomerHasDesign;
                entity.PrintingTypeId = orderDetail.PrintingTypeId;
                
                context.SaveChanges();
            }
        }

        public int InsertOrderDetail(OrderItemlBO orderDetail)
        {
            using (var context = new InThuDoEntities())
            {
                var entity = Mapper.Map<OrderItemlBO, OrderItem>(orderDetail);
                context.OrderItems.Add(entity);
                context.Entry(entity).State = System.Data.EntityState.Added;
                context.SaveChanges();
                return entity.OrderItemId;
            }
        }

        public void MarkOrderDetailAsDeleted(int orderDetailId)
        {
            using (var contex = new InThuDoEntities())
            {
                OrderItem orderDetail = contex.OrderItems.Where(od => od.OrderItemId == orderDetailId).FirstOrDefault();
                if (orderDetail == null) return;
                orderDetail.Deleted = true;
                contex.SaveChanges();
            }
        }

        public OrderItemlBO MapOrderDetail(OrderItem oi)
        {
            if (oi == null) return null;
            return new OrderItemlBO()
            {
                OrderItemId = oi.OrderItemId,
                ProductId = oi.ProductId,
                Specification = oi.Specification,
                Quantity = oi.Quantity,
                Price = oi.Price,
                CreatedBy = oi.CreatedBy,
                CreatedOn = oi.CreatedOn,
                LastEditedBy = oi.LastEditedBy,
                LastEditedOn = oi.LastEditedOn,
                Deleted = oi.Deleted,
                OrderId = oi.OrderId,
                ProductName = oi.Product.Name,
                IsCustomerHasDesign = oi.IsCustomerHasDesign,
                PrintingTypeId = oi.PrintingTypeId,
            };
        }

        public List<OrderItemlBO> MapOrderDetailList(IQueryable<OrderItem> orderItems)
        {
            List<OrderItemlBO> result = new List<OrderItemlBO>();
            foreach (OrderItem oi in orderItems)
            {
                result.Add(this.MapOrderDetail(oi));
            }
            return result;
        }

        #endregion Order Item

        #region Utils

        private OrderStatusEnum GetOrderStatus(int orderId)
        {
            using (var context = new InThuDoEntities())
            {
                OrderStatusEnum status = OrderStatusEnum.NotCompleted;
                Order order = context.Orders.Where(o => o.OrderId == orderId && (o.Deleted == false || o.Deleted == null)).FirstOrDefault();
                if (order != null)
                {
                    List<OrderItemStatusEnum> statuslist = new List<OrderItemStatusEnum>();
                    foreach (OrderItem oi in order.OrderItems)
                    {
                        if (oi.Deleted == false || oi.Deleted == null)
                        {
                            statuslist.Add(this.GetOrderItemStatusIncludedOverDue(oi.OrderItemId));
                        }
                    }

                    int i = 0;
                    int j = 0;
                    int k = 0;
                    foreach (OrderItemStatusEnum odse in statuslist)
                    {
                        if (odse == OrderItemStatusEnum.CustomerApproved)
                        {
                            i++;
                        }

                        if (odse == OrderItemStatusEnum.CustomerRefused)
                        {
                            j++;
                        }

                        if (odse == OrderItemStatusEnum.Overdue)
                        {
                            k++;
                        }
                    }

                    if (i == statuslist.Count)
                    {
                        status = OrderStatusEnum.Completed;
                    }

                    if (j > 0)
                    {
                        status = OrderStatusEnum.IsFailed;
                    }

                    if (k > 0)
                    {
                        status = OrderStatusEnum.Overdue;
                    }
                }
                return status;
            }
        }

        public decimal GetOrderTotal(OrderBO order)
        {
            using (var context = new InThuDoEntities())
            {
                var orderDetail = from od in context.OrderItems
                                  where od.OrderId == order.OrderId
                                  && (od.Deleted == false || od.Deleted == null)
                                  select od;

                decimal orderTotal = 0;
                if (orderDetail != null)
                {
                    foreach (OrderItem oi in orderDetail.ToList())
                    {
                        orderTotal += oi.Quantity * oi.Price;
                    }

                }
                return orderTotal;
            }
        }

        public OrderItemStatusEnum GetOrderItemStatus(int orderDetailId)
        {
            using (var context = new InThuDoEntities())
            {
                OrderItemStatusEnum status = OrderItemStatusEnum.DesignRequestNotCreated;
                OrderItem orderItem = context.OrderItems.Where(oi => oi.OrderItemId == orderDetailId && (oi.Deleted == null || oi.Deleted == false)).FirstOrDefault();

                if (orderItem != null)
                {
                    if (orderItem.DesignRequests.Count > 0)
                    {
                        foreach (DesignRequest dr in orderItem.DesignRequests)
                        {
                            if ((dr.Deleted == null || dr.Deleted == false) && (dr.BeginDate == null) && (dr.EndDate == null))
                            {
                                status = OrderItemStatusEnum.DesignRequestCreated;
                            }

                            if ((dr.Deleted == null || dr.Deleted == false) && (dr.BeginDate != null) && (dr.EndDate == null))
                            {
                                status = OrderItemStatusEnum.Designing;
                            }

                            if ((dr.Deleted == null || dr.Deleted == false) && (dr.BeginDate != null) && (dr.EndDate != null))
                            {
                                status = OrderItemStatusEnum.DesignCopmleted;
                            }

                            if ((dr.Deleted == null || dr.Deleted == false) && (dr.ApprovedByCustomer == true))
                            {
                                status = OrderItemStatusEnum.DesignApprovedByCustomer;
                            }

                            if (dr.ManufactureRequests.Count > 0)
                            {
                                foreach (ManufactureRequest mr in dr.ManufactureRequests)
                                {
                                    if ((mr.Deleted == null || mr.Deleted == false) && (mr.BeginDate == null) && (mr.EndDate == null))
                                    {
                                        status = OrderItemStatusEnum.ManufactureRequestCreated;
                                    }

                                    if ((mr.Deleted == null || mr.Deleted == false) && (mr.BeginDate != null) && (mr.EndDate == null))
                                    {
                                        status = OrderItemStatusEnum.Manufacturing;
                                    }

                                    if ((mr.Deleted == null || mr.Deleted == false) && (mr.BeginDate != null) && (mr.EndDate != null))
                                    {
                                        status = OrderItemStatusEnum.ManufactureCompleted;
                                    }

                                    if ((mr.Deleted == null || mr.Deleted == false) && (mr.BeginDate != null) && (mr.EndDate != null)&&(mr.CustomerApproved==true))
                                    {
                                        status = OrderItemStatusEnum.CustomerApproved;
                                    }

                                    if ((mr.Deleted == null || mr.Deleted == false) && (mr.BeginDate != null) && (mr.EndDate != null) && (mr.IsFailed == true))
                                    {
                                        status = OrderItemStatusEnum.CustomerRefused;
                                    }
                                }
                            }
                        }
                    }
                }

                return status;
            }

        }

        public OrderItemStatusEnum GetOrderItemStatusIncludedOverDue(int orderDetailId)
        {
            using (var context = new InThuDoEntities())
            {
                OrderItemStatusEnum status = OrderItemStatusEnum.DesignRequestNotCreated;
                OrderItem orderItem = context.OrderItems.Where(oi => oi.OrderItemId == orderDetailId && (oi.Deleted == null || oi.Deleted == false)).FirstOrDefault();
                if (orderItem != null)
                {
                    if (orderItem.Order.ExpectedCompleteDate.HasValue)
                    {
                        DateTime expectedDate = DateTime.Parse(orderItem.Order.ExpectedCompleteDate.ToString());
                        expectedDate = expectedDate.AddHours(23);
                        expectedDate = expectedDate.AddMinutes(59);
                        expectedDate = expectedDate.AddSeconds(59);
                        if (expectedDate < DateTime.Now)
                        {
                            var orderDetailStatus = this.GetOrderItemStatus(orderDetailId);
                            if (orderDetailStatus < OrderItemStatusEnum.CustomerApproved)
                            {
                                status = OrderItemStatusEnum.Overdue;
                            }
                            else
                            {
                                status = orderDetailStatus;
                            }
                        }
                        else
                        {
                            status = this.GetOrderItemStatus(orderDetailId);
                        }
                    }
                    else
                    {
                        status = this.GetOrderItemStatus(orderDetailId);
                    }
                }

                return status;
            }
        }

        public List<OrderStatusBO> GetAllOrderStatus()
        {
            using (var context = new InThuDoEntities())
            {
                return (from status in context.LibOrderStatus
                        select new OrderStatusBO()
                        {
                            OrderStatusId = status.OrderStatusId,
                            Name = status.Name,
                            Description = status.Description,
                            ProcessingOrder = status.ProcessingOrder
                        }).ToList();
            }
        }

        public List<DepositMethodBO> GetAllDepositMethod()
        {
            using (var context = new InThuDoEntities())
            {
                return (from d in context.LibDepositTypes
                        select new DepositMethodBO()
                        {
                            DepositTypeId = d.DepositTypeId,
                            Name = d.Name,
                            Description = d.Description
                        }).ToList();
            }
        }

        public List<ShippingMethodBO> GetAllShippingMethod()
        {
            using (var context = new InThuDoEntities())
            {

                return (from s in context.LibShippingMethods
                        select new ShippingMethodBO()
                        {
                            Name = s.Name,
                            Description = s.Description,
                            ShippingMethodId = s.ShippingMethodId
                        }).ToList();

            }
        }

        public OrderStatusBO GetContinueOrderStatusByOrderId(int orderId)
        {
            throw new NotImplementedException();
        }

        public List<PrintingTypeBO> GetAllPrintingType()
        {
            using (var context = new InThuDoEntities())
            {
                var query = (from pt in context.LibPrintingTypes
                            select pt).ToList();

                List<PrintingTypeBO> printingTypes = new List<PrintingTypeBO>();
                foreach (LibPrintingType p in query)
                {
                    PrintingTypeBO printingType = new PrintingTypeBO()
                    {
                        Id = p.Id,
                        Code = p.Code,
                        Name = p.Name,
                        Description = p.Description,
                    };
                    printingTypes.Add(printingType);
                }

                return printingTypes;
            }
        }

        public void InsertOrderStatusMapping(OrderStatusMappingBO orderStatusMapping)
        {
            using (var context = new InThuDoEntities())
            {
                var entity = Mapper.Map<OrderStatusMappingBO, OrderStatusMapping>(orderStatusMapping);
                context.OrderStatusMappings.Add(entity);
                context.SaveChanges();
            }
        }

        public List<OrganizationBO> GetAllOrganizations()
        {
            using (var context = new InThuDoEntities())
            {
                var query = from og in context.Organizations
                            select new OrganizationBO()
                            {
                                OrganizationId = og.OrganizationId,
                                Name = og.Name
                            };
                return query.Distinct().ToList();
            }
        }

        public PrintingTypeBO GetPrintTypeByCode(string code)
        {
            using (var context = new InThuDoEntities())
            {
                var query = (from p in context.LibPrintingTypes
                            where p.Code == code
                            select new PrintingTypeBO() { 
                                Id = p.Id,
                                Code=p.Code,
                                Name = p.Name,
                                Description = p.Description
                            }).FirstOrDefault();
                return query;
            }
        }

        #endregion Utils     

        #region DesignRequest

        public DesignRequestBO GetDesignRequestById(int requestId)
        {
            using (var context = new InThuDoEntities())
            {
                return (from dr in context.DesignRequests
                        where
                        (dr.DesignRequestId == requestId)&&(dr.Deleted==null|| dr.Deleted == false)
                        select new DesignRequestBO()
                        {
                            DesignRequestId = dr.DesignRequestId,
                            Description = dr.Description,
                            DesignerId = dr.DesignerId,
                            BeginDate = dr.BeginDate,
                            EndDate = dr.EndDate,
                            Cost = dr.Cost,
                            CreatedBy = dr.CreatedBy,
                            CreatedOn = dr.CreatedOn,
                            LastEditedBy = dr.LastEditedBy,
                            LastEditedOn = dr.LastEditedOn,
                            OrderItem = new OrderItemlBO() { 
                                OrderItemId = dr.OrderItemId,
                                ProductId = dr.OrderItem.ProductId,
                                Specification = dr.OrderItem.Specification,
                                Quantity = dr.OrderItem.Quantity,
                                Price = dr.OrderItem.Price,
                                CreatedBy = dr.OrderItem.CreatedBy,
                                CreatedOn = dr.OrderItem.CreatedOn,
                                LastEditedBy = dr.OrderItem.LastEditedBy,
                                LastEditedOn = dr.OrderItem.LastEditedOn,
                                OrderId = dr.OrderItem.OrderId,                                
                                ProductName = dr.OrderItem.Product.Name
                            },
                            ApprovedByCustomer = dr.ApprovedByCustomer,
                            Note = dr.Note,
                            ApprovedDate = dr.ApprovedDate,
                            OrderItemId = dr.OrderItemId,
                        }).FirstOrDefault();
            }
        }

        public void UpdateDesignRequest(DesignRequestBO designReq)
        {
            using (var context = new InThuDoEntities())
            {
                DesignRequest ds = context.DesignRequests.Where(d => d.DesignRequestId == designReq.DesignRequestId).FirstOrDefault();
                ds.Description = designReq.Description;
                ds.DesignerId = designReq.DesignerId;
                ds.BeginDate = designReq.BeginDate;
                ds.EndDate = designReq.EndDate;
                ds.Cost = designReq.Cost;
                ds.LastEditedBy = designReq.LastEditedBy;
                ds.LastEditedOn = designReq.LastEditedOn;
                ds.ApprovedByCustomer = designReq.ApprovedByCustomer;
                ds.Note = designReq.Note;
                ds.ApprovedDate = designReq.ApprovedDate;

                context.SaveChanges();
            }
        }

        public int InsertDesignRequest(DesignRequestBO designReq)
        {
            using (var context = new InThuDoEntities())
            {
                DesignRequest ds = new DesignRequest()
                {
                    Description = designReq.Description,
                    DesignerId = designReq.DesignerId,
                    BeginDate = designReq.BeginDate,
                    EndDate = designReq.EndDate,
                    Cost = designReq.Cost,
                    CreatedBy = designReq.CreatedBy,
                    CreatedOn= designReq.CreatedOn,
                    OrderItemId = designReq.OrderItemId
                };

                context.DesignRequests.Add(ds);
                context.Entry(ds).State = System.Data.EntityState.Added;
                context.SaveChanges();

                return ds.DesignRequestId;
            }
        }

        public void MaskDesignRequestAsDeleted(int designRequestId, int deletedBy)
        {
            using (var context = new InThuDoEntities())
            {
                DesignRequest ds = context.DesignRequests.Where(d => d.DesignRequestId == designRequestId).FirstOrDefault();
                ds.Deleted = true;
                ds.LastEditedBy = deletedBy;
                ds.LastEditedOn = DateTime.Now;
                context.SaveChanges();
            }
        }

        public DesignRequestBO GetDesignRequestByOrderDetailId(int orderDetailId)
        {
            using (var context = new InThuDoEntities())
            {
                var query = from dr in context.DesignRequests
                            where
                            (dr.OrderItemId == orderDetailId) &&
                            (dr.Deleted == null || dr.Deleted == false)
                            select new DesignRequestBO()
                            {
                                DesignRequestId = dr.DesignRequestId,
                                Description = dr.Description,
                                DesignerId = dr.DesignerId,
                                BeginDate = dr.BeginDate,
                                EndDate = dr.EndDate,
                                Cost = dr.Cost,
                                CreatedBy = dr.CreatedBy,
                                CreatedOn = dr.CreatedOn,
                                LastEditedBy = dr.LastEditedBy,
                                LastEditedOn = dr.LastEditedOn,
                                OrderItem = new OrderItemlBO()
                                {
                                    OrderItemId = dr.OrderItemId,
                                    ProductId = dr.OrderItem.ProductId,
                                    Specification = dr.OrderItem.Specification,
                                    Quantity = dr.OrderItem.Quantity,
                                    Price = dr.OrderItem.Price,
                                    CreatedBy = dr.OrderItem.CreatedBy,
                                    CreatedOn = dr.OrderItem.CreatedOn,
                                    LastEditedBy = dr.OrderItem.LastEditedBy,
                                    LastEditedOn = dr.OrderItem.LastEditedOn,
                                    OrderId = dr.OrderItem.OrderId,                                   
                                    ProductName = dr.OrderItem.Product.Name
                                },
                                ApprovedByCustomer =dr.ApprovedByCustomer,
                                ApprovedDate = dr.ApprovedDate,
                                Note = dr.Note
                            };
                return query.FirstOrDefault();
            }
        }

        public List<DesignRequestBO> GetDesignRequests(DesignRequestSearch searchObj)
        {
            using (var context = new InThuDoEntities())
            {
                var query = (from dr in context.DesignRequests
                             join od in context.OrderItems on dr.OrderItemId equals od.OrderItemId
                             join o in context.Orders on od.OrderId equals o.OrderId
                             where
                             (dr.Deleted == null || dr.Deleted == false) &&
                             (searchObj.CustomerId == 0 || o.CustomerId == searchObj.CustomerId) &&
                             (searchObj.ProductId == 0 || od.ProductId == searchObj.ProductId) &&
                             (searchObj.DesignerId == 0 || dr.DesignerId == searchObj.DesignerId) &&
                             (searchObj.RequestFrom == null || dr.BeginDate >= searchObj.RequestFrom) &&
                             (searchObj.RequestTo == null || dr.EndDate <= searchObj.RequestTo)
                             select new DesignRequestBO()
                             {
                                 DesignRequestId = dr.DesignRequestId,
                                 Description = dr.Description,
                                 DesignerId = dr.DesignerId,
                                 BeginDate = dr.BeginDate,
                                 EndDate = dr.EndDate,
                                 Cost = dr.Cost,
                                 CreatedBy = dr.CreatedBy,
                                 CreatedOn = dr.CreatedOn,
                                 LastEditedBy = dr.LastEditedBy,
                                 LastEditedOn = dr.LastEditedOn,
                                 OrderItemId = dr.OrderItemId,
                                 ApprovedByCustomer = dr.ApprovedByCustomer,
                                 Note = dr.Note,
                                 ApprovedDate = dr.ApprovedDate,
                                 OrderId = o.OrderId,
                                 OrderDate = o.OrderDate,
                                 ProductName = od.Product.Name,
                                 OrderItem = new OrderItemlBO()
                                 {
                                     OrderItemId = od.OrderItemId
                                 }
                             }).Distinct().ToList();

                var orders = from o in context.Orders
                             join od in context.OrderItems on o.OrderId equals od.OrderId
                             join dr in context.DesignRequests on od.OrderItemId equals dr.OrderItemId
                             select o;

                var orderDetails = (from od in context.OrderItems
                                    join o in orders on od.OrderId equals o.OrderId
                                    where
                                    (od.Deleted == false || od.Deleted == null)
                                    select new OrderItemlBO()
                                    {
                                        OrderItemId = od.OrderItemId,
                                    }).Distinct().ToList();
                foreach (OrderItemlBO od in orderDetails)
                {
                    od.OrderItemStatus = this.GetOrderItemStatusIncludedOverDue(od.OrderItemId);
                }

                if (searchObj.DesignRequestStatus != 0)
                {
                    query = (from q in query                          
                             where
                             q.DesignRequestStatus == searchObj.DesignRequestStatus
                             select q).ToList();

                }

                foreach (DesignRequestBO dr in query)
                {
                    dr.OrderItem.OrderItemStatus = this.GetOrderItemStatusIncludedOverDue(dr.OrderItem.OrderItemId);
                }

                return query;

            }
        }

        #endregion DesignRequest

        #region ManufactureRequest
        
        public List<ManufactureRequestBO> GetManufactureRequests(ManufactureRequestSearch manuSearchObj)
        {
            using (var context = new InThuDoEntities())
            {
                var query = from m in context.ManufactureRequests
                            join ds in context.DesignRequests on m.DesignRequestId equals ds.DesignRequestId
                            join oi in context.OrderItems on ds.OrderItemId equals oi.OrderItemId
                            join o in context.Orders on oi.OrderId equals o.OrderId
                            where
                            (m.Deleted == false || m.Deleted == null) &&
                            (ds.Deleted == false || ds.Deleted == null) &&
                            (oi.Deleted == false || oi.Deleted == null) &&
                            (o.Deleted == false || o.Deleted == null) &
                            (manuSearchObj.From == null || manuSearchObj.From <= m.CreatedOn) &&
                            (manuSearchObj.To == null || manuSearchObj.To >= m.CustomerApprovedDate || manuSearchObj.To >= m.EndDate || manuSearchObj.To >= m.BeginDate) &&
                            (manuSearchObj.CustomerId == 0 || manuSearchObj.CustomerId == o.CustomerId) &&
                            (manuSearchObj.ProductId == 0 || manuSearchObj.ProductId == oi.ProductId)&&
                            (manuSearchObj.DesignerId ==0 || manuSearchObj.DesignerId == ds.DesignerId)&&
                            (manuSearchObj.BusinessManId ==0 || manuSearchObj.BusinessManId == o.UserId)&&
                            (manuSearchObj.ManufactureId == 0 || manuSearchObj.ManufactureId == m.ManufactureId)&&
                            (manuSearchObj.PrintTypeIds.Count ==0 || manuSearchObj.PrintTypeIds.Contains(oi.PrintingTypeId))&&
                            (manuSearchObj.OrderId ==0 || manuSearchObj.OrderId == o.OrderId)
                            select m;

                List<ManufactureRequestBO> manuRequests = this.MapManufactureRequestList(query);
                if (manuSearchObj.ManufactureRequestStatus != 0)
                {
                    manuRequests = manuRequests.Where(m => m.ManufactureRequestStatus == manuSearchObj.ManufactureRequestStatus).ToList();
                }

                return manuRequests;
            }
        }

        public ManufactureRequestBO GetManufactureRequestById(int id)
        {
            using (var context = new InThuDoEntities())
            {
                var query = (from m in context.ManufactureRequests
                       where m.ManufactureRequestId == id
                       select m).FirstOrDefault();

                return this.MapManufactureRequest(query);
            }
        }

        public void UpdateManufactureRequest(ManufactureRequestBO manu)
        {
            using (var context = new InThuDoEntities())
            {
                ManufactureRequest ma = context.ManufactureRequests.Where(m => m.ManufactureRequestId == manu.ManufactureRequestId).FirstOrDefault();
                if (ma == null) return;
                ma.Description = manu.Description;
                ma.BeginDate = manu.BeginDate;
                ma.EndDate = manu.EndDate;
                ma.Quantity = manu.Quantity;
                ma.Cost = manu.Cost;
                ma.LastEditedBy = manu.LastEditedBy;
                ma.LastEditedOn = manu.LastEditedOn;
                ma.CustomerApproved = manu.CustomerApproved;
                ma.CustomerApprovedDate = manu.CustomerApprovedDate;
                ma.Note = manu.Note;
                ma.CustomerApprovedPrice = manu.CustomerApprovedPrice;
                ma.CustomerApprovedQuantity = manu.CustomerApprovedQuantity;
                ma.IsFailed = manu.IsFailed;
                ma.ManufactureId = manu.ManufactureId;

                context.SaveChanges();
            }
        }

        public int InsertManufactureRequest(ManufactureRequestBO manu)
        {
            using (var context = new InThuDoEntities())
            {
                ManufactureRequest ma = new ManufactureRequest()
                {
                    Description = manu.Description,
                    BeginDate = manu.BeginDate,
                    EndDate = manu.EndDate,
                    Quantity = manu.Quantity,
                    Cost = manu.Cost,
                    CreatedBy = manu.CreatedBy,
                    CreatedOn = manu.CreatedOn,
                    DesignRequestId = manu.DesignRequestId,
                    ManufactureId = manu.ManufactureId
                };

                context.ManufactureRequests.Add(ma);
                context.SaveChanges();

                return ma.ManufactureRequestId;
            }
        }

        public ManufactureRequestBO GetManufactureRequestByDesignRequest(int designRequestId)
        {
            using (var context = new InThuDoEntities())
            {
                var query = (from m in context.ManufactureRequests
                            where m.DesignRequestId == designRequestId
                            &&(m.Deleted == false || m.Deleted == null)
                             select m).FirstOrDefault();
                if (query == null) return null;

                return this.MapManufactureRequest(query);                
            }
        }

        public void MarkManufactureRequestAsDeleted(int manuRequestId)
        {
            using (var context = new InThuDoEntities())
            {
                var manu = (from m in context.ManufactureRequests
                            where m.ManufactureRequestId == manuRequestId
                            select m).FirstOrDefault();
                manu.Deleted = true;
                context.SaveChanges();
            }
        }

        private ManufactureRequestBO MapManufactureRequest(ManufactureRequest m)
        {
            if (m == null) return null;

            var manu = new ManufactureRequestBO()
                       {
                           ManufactureRequestId = m.ManufactureRequestId,
                           DesignRequestId = m.DesignRequestId,
                           Description = m.Description,
                           BeginDate = m.BeginDate,
                           EndDate = m.EndDate,
                           Cost = m.Cost,
                           CreatedBy = m.CreatedBy,
                           CreatedOn = m.CreatedOn,
                           LastEditedBy = m.LastEditedBy,
                           LastEditedOn = m.LastEditedOn,
                           Deleted = m.Deleted,
                           Quantity = m.Quantity,
                           CustomerApproved = m.CustomerApproved,
                           CustomerApprovedDate = m.CustomerApprovedDate,
                           Note = m.Note,
                           CustomerApprovedPrice = m.CustomerApprovedPrice,
                           CustomerApprovedQuantity = m.CustomerApprovedQuantity,
                           IsFailed = m.IsFailed,
                           ManufactureId = m.ManufactureId,
                           Manufacture = new CustomerBO { 
                                CustomerId = m.ManufactureId,
                                Name = m.Customer.Name
                           },
                           ProductName = m.DesignRequest.OrderItem.Product.Name,
                           PrintingTypeName = m.DesignRequest.OrderItem.LibPrintingType.Name,
                           BusinessMan = m.DesignRequest.OrderItem.Order.User.FullName,
                           OrderId = m.DesignRequest.OrderItem.Order.OrderId,
                           CustomterName = m.DesignRequest.OrderItem.Order.Customer.Name
                       };

            
            return manu;
        }

        private List<ManufactureRequestBO> MapManufactureRequestList(IQueryable<ManufactureRequest> manuRequests)
        {
            List<ManufactureRequestBO> result = new List<ManufactureRequestBO>();

            foreach (ManufactureRequest m in manuRequests)
            {
                result.Add(this.MapManufactureRequest(m));
            }
            return result;
        }

        #endregion ManufactureRequest

        #region Approved Products

        public List<ProductApprovedBO> GetApprovedProductByOrderId(int orderId)
        {
            using (var context = new InThuDoEntities())
            { 
                var query = (from o in context.Orders
                            join od in context.OrderItems on o.OrderId equals od.OrderId
                            join dr in context.DesignRequests on od.OrderItemId equals dr.OrderItemId
                            join mr in context.ManufactureRequests on dr.DesignRequestId equals mr.DesignRequestId
                            where 
                            (od.Deleted == false || od.Deleted == null)&&
                            (dr.Deleted == false || dr.Deleted == null)&&
                            (mr.Deleted == false || mr.Deleted == null)&&
                            (o.OrderId == orderId)&&
                            mr.CustomerApproved == true
                            select new ProductApprovedBO(){
                                ProductName = od.Product.Name,
                                Quantity = mr.CustomerApprovedQuantity,
                                Price = mr.CustomerApprovedPrice
                            }).Distinct().ToList();
                return query;

            }
        }

        public List<ProductApprovedBO> GetFailedProductByOrderId(int orderId)
        {
            using (var context = new InThuDoEntities())
            {
                var query = (from o in context.Orders
                             join od in context.OrderItems on o.OrderId equals od.OrderId
                             join dr in context.DesignRequests on od.OrderItemId equals dr.OrderItemId
                             join mr in context.ManufactureRequests on dr.DesignRequestId equals mr.DesignRequestId
                             where
                             (od.Deleted == false || od.Deleted == null) &&
                             (dr.Deleted == false || dr.Deleted == null) &&
                             (mr.Deleted == false || mr.Deleted == null) &&
                             (o.OrderId == orderId) &&
                             mr.IsFailed == true
                             select new ProductApprovedBO()
                             {
                                 ProductName = od.Product.Name,
                                 Quantity = od.Quantity,
                                 Price = od.Price
                             }).Distinct().ToList();
                return query;

            }
        }

        #endregion Approved Products

    }
}
