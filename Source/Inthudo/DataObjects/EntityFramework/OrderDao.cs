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
            Mapper.CreateMap<OrderItem, OrderDetailBO>();
            Mapper.CreateMap<OrderDetailBO, OrderItem>();
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
                                 UserId = o.UserId,
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
                var entity = Mapper.Map<OrderBO, Order>(order);
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
                entity.UserId = order.UserId;
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
                             join oitem in context.OrderItems on o.OrderId equals oitem.OrderId into orderItemGrop
                             from oitem2 in orderItemGrop.DefaultIfEmpty()
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
                             (orderSearchObj.DesignerManId == 0 || oitem2.DesignerId == orderSearchObj.DesignerManId) &&
                             (orderSearchObj.OrderFrom == null || o.OrderDate >= orderSearchObj.OrderFrom) &&
                             (orderSearchObj.OrderTo == null || o.OrderDate <= orderSearchObj.OrderTo) &&
                             (orderSearchObj.OrganizationId == 0 || or.OrganizationId == orderSearchObj.OrganizationId) &&
                             (o.Deleted == null || o.Deleted == false)
                             select new OrderBO
                             {
                                 OrderId = o.OrderId,
                                 OrderDate = o.OrderDate,
                                 Deposit = o.Deposit,
                                 DepositTypeId = o.DepositTypeId,
                                 ShippingMethodId = o.ShippingMethodId,
                                 CustomerId = o.CustomerId,
                                 UserId = o.UserId,
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
                                      select new OrderDetailBO()
                                      {
                                          OrderItemId = od.OrderItemId,
                                          OrderId = od.OrderId,
                                          Quantity= od.Quantity,
                                          Price = od.Price
                                      }).ToList();
                    if (orderItems.Count > 0)
                    {
                        foreach (OrderDetailBO od in orderItems)
                        {
                            od.OrderDetailStatus = this.GetOrderDetailStatusIncludedOverDue(od.OrderItemId);
                        }

                        o.OrderItems = orderItems.Distinct().ToList();
                    }


                }

                if (orderSearchObj.OrderDetailStatus != 0)
                {
                    var orderItems = (from od in context.OrderItems
                                      where
                                       (od.Deleted == null || od.Deleted == false)

                                      select new OrderDetailBO()
                                      {
                                          OrderItemId = od.OrderItemId,
                                          OrderId = od.OrderId,
                                          Quantity = od.Quantity,
                                          Price = od.Price
                                      }).ToList();
                    if (orderItems.Count > 0)
                    {
                        foreach (OrderDetailBO od in orderItems)
                        {
                            od.OrderDetailStatus = this.GetOrderDetailStatusIncludedOverDue(od.OrderItemId);
                        }
                    }

                    query = (from q in query
                             join od in orderItems on q.OrderId equals od.OrderId
                             where od.OrderDetailStatus == orderSearchObj.OrderDetailStatus
                             select q).Distinct().ToList();
                }

                if (orderSearchObj.OrderStatus != 0)
                {
                    query = (from q in query
                             where q.OrderStatus == orderSearchObj.OrderStatus
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

        #region OrderDetail

        public OrderDetailBO GetOrderDetailById(int id)
        {
            using (var context = new InThuDoEntities())
            {
                var orderDetail = (from od in context.OrderItems
                                   where od.OrderItemId == id &&
                                   (od.Deleted == null || od.Deleted == false)
                                   select new OrderDetailBO()
                                   {
                                       OrderItemId = od.OrderItemId,
                                       ProductId = od.ProductId,
                                       Specification = od.Specification,
                                       Quantity = od.Quantity,
                                       Price = od.Price,
                                       CreatedBy = od.CreatedBy,
                                       CreatedOn = od.CreatedOn,
                                       LastEditedBy = od.LastEditedBy,
                                       LastEditedOn = od.LastEditedOn,
                                       OrderId = od.OrderId,
                                       DesignerId = od.DesignerId,
                                       ProductName = od.Product.Name
                                   }).FirstOrDefault();

                if (orderDetail != null)
                {
                    orderDetail.OrderDetailStatus = this.GetOrderDetailStatusIncludedOverDue(orderDetail.OrderItemId);
                }
                return orderDetail;
            }
        }

        public List<OrderDetailBO> GetOrderDetailsByOrderId(int orderId)
        {
            using (var context = new InThuDoEntities())
            {
                var orderDetails = (from od in context.OrderItems
                        where
                        (od.OrderId == orderId) &&
                        (od.Deleted == null || od.Deleted == false)
                        select new OrderDetailBO()
                        {
                            OrderItemId = od.OrderItemId,
                            ProductId = od.ProductId,
                            Specification = od.Specification,
                            Quantity = od.Quantity,
                            Price = od.Price,
                            CreatedBy = od.CreatedBy,
                            CreatedOn = od.CreatedOn,
                            LastEditedBy = od.LastEditedBy,
                            LastEditedOn = od.LastEditedOn,
                            Deleted = od.Deleted,
                            OrderId = od.OrderId,
                            ProductName = od.Product.Name
                        }).ToList();
                foreach (OrderDetailBO od in orderDetails)
                {
                    od.OrderDetailStatus = this.GetOrderDetailStatusIncludedOverDue(od.OrderItemId);
                }

                return orderDetails;
            }
        }

        public void UpdateOrderDetail(OrderDetailBO orderDetail)
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
                entity.DesignerId = orderDetail.DesignerId;
                context.SaveChanges();
            }
        }

        public int InsertOrderDetail(OrderDetailBO orderDetail)
        {
            using (var context = new InThuDoEntities())
            {
                var entity = Mapper.Map<OrderDetailBO, OrderItem>(orderDetail);
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

        #endregion OrderDetail

        #region Utils

        private OrderStatusEnum GetOrderStatus(int orderId)
        {
            using (var context = new InThuDoEntities())
            {
                OrderStatusEnum status = OrderStatusEnum.NotCompleted;
                Order order = context.Orders.Where(o => o.OrderId == orderId && (o.Deleted == false || o.Deleted == null)).FirstOrDefault();
                if (order != null)
                {
                    List<OrderDetailStatusEnum> statuslist = new List<OrderDetailStatusEnum>();
                    foreach (OrderItem oi in order.OrderItems)
                    {
                        if (oi.Deleted == false || oi.Deleted == null)
                        {
                            statuslist.Add(this.GetOrderDetailStatusIncludedOverDue(oi.OrderItemId));
                        }
                    }

                    int i = 0;
                    int j = 0;
                    int k = 0;
                    foreach (OrderDetailStatusEnum odse in statuslist)
                    {
                        if (odse == OrderDetailStatusEnum.CustomerApproved)
                        {
                            i++;
                        }

                        if (odse == OrderDetailStatusEnum.CustomerRefused)
                        {
                            j++;
                        }

                        if (odse == OrderDetailStatusEnum.Overdue)
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

        public OrderDetailStatusEnum GetOrderDetailStatus(int orderDetailId)
        {
            using (var context = new InThuDoEntities())
            {
                OrderDetailStatusEnum status = OrderDetailStatusEnum.DesignRequestNotCreated;
                OrderItem orderItem = context.OrderItems.Where(oi => oi.OrderItemId == orderDetailId && (oi.Deleted == null || oi.Deleted == false)).FirstOrDefault();

                if (orderItem != null)
                {
                    if (orderItem.DesignRequests.Count > 0)
                    {
                        foreach (DesignRequest dr in orderItem.DesignRequests)
                        {
                            if ((dr.Deleted == null || dr.Deleted == false) && (dr.BeginDate == null) && (dr.EndDate == null))
                            {
                                status = OrderDetailStatusEnum.DesignRequestCreated;
                            }

                            if ((dr.Deleted == null || dr.Deleted == false) && (dr.BeginDate != null) && (dr.EndDate == null))
                            {
                                status = OrderDetailStatusEnum.Designing;
                            }

                            if ((dr.Deleted == null || dr.Deleted == false) && (dr.BeginDate != null) && (dr.EndDate != null))
                            {
                                status = OrderDetailStatusEnum.DesignCopmleted;
                            }

                            if ((dr.Deleted == null || dr.Deleted == false) && (dr.ApprovedByCustomer == true))
                            {
                                status = OrderDetailStatusEnum.DesignApprovedByCustomer;
                            }

                            if (dr.ManufactureRequests.Count > 0)
                            {
                                foreach (ManufactureRequest mr in dr.ManufactureRequests)
                                {
                                    if ((mr.Deleted == null || mr.Deleted == false) && (mr.BeginDate == null) && (mr.EndDate == null))
                                    {
                                        status = OrderDetailStatusEnum.ManufactureRequestCreated;
                                    }

                                    if ((mr.Deleted == null || mr.Deleted == false) && (mr.BeginDate != null) && (mr.EndDate == null))
                                    {
                                        status = OrderDetailStatusEnum.Manufacturing;
                                    }

                                    if ((mr.Deleted == null || mr.Deleted == false) && (mr.BeginDate != null) && (mr.EndDate != null))
                                    {
                                        status = OrderDetailStatusEnum.ManufactureCompleted;
                                    }

                                    if ((mr.Deleted == null || mr.Deleted == false) && (mr.BeginDate != null) && (mr.EndDate != null)&&(mr.CustomerApproved==true))
                                    {
                                        status = OrderDetailStatusEnum.CustomerApproved;
                                    }

                                    if ((mr.Deleted == null || mr.Deleted == false) && (mr.BeginDate != null) && (mr.EndDate != null) && (mr.IsFailed == true))
                                    {
                                        status = OrderDetailStatusEnum.CustomerRefused;
                                    }
                                }
                            }
                        }
                    }
                }

                return status;
            }

        }

        public OrderDetailStatusEnum GetOrderDetailStatusIncludedOverDue(int orderDetailId)
        {
            using (var context = new InThuDoEntities())
            {
                OrderDetailStatusEnum status = OrderDetailStatusEnum.DesignRequestNotCreated;
                OrderItem orderItem = context.OrderItems.Where(oi => oi.OrderItemId == orderDetailId && (oi.Deleted == null || oi.Deleted == false)).FirstOrDefault();
                if (orderItem != null)
                {
                    if (orderItem.Order.ExpectedCompleteDate < DateTime.Now)
                    {
                        var orderDetailStatus = this.GetOrderDetailStatus(orderDetailId);
                        if (orderDetailStatus < OrderDetailStatusEnum.CustomerApproved)
                        {
                            status = OrderDetailStatusEnum.Overdue;
                        }
                        else
                        {
                            status = orderDetailStatus;
                        }
                    }
                    else
                    {
                        status = this.GetOrderDetailStatus(orderDetailId);
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
                            OrderItem = new OrderDetailBO() { 
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
                                DesignerId = dr.OrderItem.DesignerId,
                                ProductName = dr.OrderItem.Product.Name
                            },
                            ApprovedByCustomer = dr.ApprovedByCustomer,
                            Note = dr.Note,
                            ApprovedDate = dr.ApprovedDate
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
                                OrderItem = new OrderDetailBO()
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
                                    DesignerId = dr.OrderItem.DesignerId,
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
                                 OrderItem = new OrderDetailBO()
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
                                    select new OrderDetailBO()
                                    {
                                        OrderItemId = od.OrderItemId,
                                    }).Distinct().ToList();
                foreach (OrderDetailBO od in orderDetails)
                {
                    od.OrderDetailStatus = this.GetOrderDetailStatusIncludedOverDue(od.OrderItemId);
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
                    dr.OrderItem.OrderDetailStatus = this.GetOrderDetailStatusIncludedOverDue(dr.OrderItem.OrderItemId);
                }

                return query;

            }
        }

        #endregion DesignRequest

        #region ManufactureRequest

        public ManufactureRequestBO GetManufactureRequestById(int id)
        {
            using (var context = new InThuDoEntities())
            {
                return (from m in context.ManufactureRequests
                       where m.ManufactureRequestId == id
                       select new ManufactureRequestBO()
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
                           IsFailed = m.IsFailed
                       }).FirstOrDefault();
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

                return new ManufactureRequestBO()
                {
                    ManufactureRequestId = query.ManufactureRequestId,
                    DesignRequestId = query.DesignRequestId,
                    Description = query.Description,
                    BeginDate = query.BeginDate,
                    EndDate = query.EndDate,
                    Cost = query.Cost,
                    CreatedBy = query.CreatedBy,
                    CreatedOn = query.CreatedOn,
                    LastEditedBy = query.LastEditedBy,
                    LastEditedOn = query.LastEditedOn,
                    Deleted = query.Deleted,
                    Quantity = query.Quantity,
                    CustomerApproved = query.CustomerApproved,
                    CustomerApprovedDate = query.CustomerApprovedDate,
                    Note = query.Note,
                    CustomerApprovedPrice = query.CustomerApprovedPrice,
                    CustomerApprovedQuantity = query.CustomerApprovedQuantity,
                    IsFailed = query.IsFailed
                };
                
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
