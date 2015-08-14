﻿using System;
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
            Mapper.CreateMap<User, Member>();
            Mapper.CreateMap<Member, User>();
            Mapper.CreateMap<OrderItem, OrderDetailBO>();
            Mapper.CreateMap<OrderDetailBO, OrderItem>();
        }

        public OrderDetailBO GetOrderDetailById(int id)
        {
            using (var context = new InThuDoEntities())
            {
                var orderDetail =  (from od in context.OrderItems
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

                
                return orderDetail;
            }
        }

        public OrderBO GetOrderById(int orderId)
        {
            using (context)
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
                        }).FirstOrDefault();               
            
                return order;
            }
        }

        public List<OrderDetailBO> GetOrderItemsByOrderId(int orderId)
        {
            using( context )
            {
                return (from od in context.OrderItems
                            where
                            (od.OrderId == orderId)&&
                            (od.Deleted == null || od.Deleted ==false)
                            select new OrderDetailBO() { 
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
            using (context )
            {               
                return (from d in context.LibDepositTypes
                        select new DepositMethodBO() { 
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

        public void InsertOrderDetail(OrderDetailBO orderDetail)
        {
            using (var context = new InThuDoEntities())
            {
                var entity = Mapper.Map<OrderDetailBO, OrderItem>(orderDetail);
                context.OrderItems.Add(entity);
                context.Entry(entity).State = System.Data.EntityState.Added;
                context.SaveChanges();
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

                context.Entry(entity).State = System.Data.EntityState.Modified;
                context.SaveChanges();
            }

            
        }

        public InThuDoEntities context
        {
            get
            {
                return new InThuDoEntities();
            }
        }


        public List<OrderBO> GetOrders(OrderSearch orderSearchObj)
        {
            using (var context = new InThuDoEntities())
            {
                var query = (from o in context.Orders
                            join oitem in context.OrderItems on o.OrderId equals oitem.OrderId  into orderItemGrop
                            from oitem2 in orderItemGrop.DefaultIfEmpty()
                            join oStatusMapping in context.OrderStatusMappings on oitem2.OrderItemId equals oStatusMapping.OrderItemId into oStatusMappingGroup
                            from oStatusMapping2 in oStatusMappingGroup.DefaultIfEmpty()
                            where
                            (oitem2.Deleted == false || oitem2.Deleted == null)&&
                            (orderSearchObj.OrderId == 0 || o.OrderId == orderSearchObj.OrderId) &&
                            (orderSearchObj.CustId == 0 || o.CustomerId == orderSearchObj.CustId) &&
                            (orderSearchObj.ProductId == 0 || oitem2.ProductId == orderSearchObj.ProductId) &&
                            (orderSearchObj.ShipMethodId ==0|| o.ShippingMethodId == orderSearchObj.ShipMethodId)&&
                            (orderSearchObj.DepositMethodId ==0 || o.DepositTypeId == orderSearchObj.DepositMethodId)&&
                            (orderSearchObj.OrderStatusId == 0 || oStatusMapping2.OrderStatusId == orderSearchObj.OrderStatusId)&&
                            (orderSearchObj.BusManId ==0 || o.UserId == orderSearchObj.BusManId)&&
                            (orderSearchObj.DesignerManId ==0 || oitem2.DesignerId == orderSearchObj.DesignerManId)&&
                            (o.Deleted ==null || o.Deleted==false)
                            select new OrderBO{ 
                                OrderId = o.OrderId,
                                OrderDate = o.OrderDate,
                                Deposit =o.Deposit,
                                DepositTypeId =o.DepositTypeId,
                                ShippingMethodId = o.ShippingMethodId,                                
                                CustomerId= o.CustomerId,
                                UserId = o.UserId,
                                CreatedBy =o.CreatedBy,
                                CreatedDate = o.CreatedDate,
                                LastEditedBy =o.LastEditedBy,
                                LastEditedDate = o.LastEditedDate,
                                CustomerName = o.Customer.Name,
                                BusinessManName = o.User.FullName,
                                DepositTypeName = o.LibDepositType.Name,
                                ShippingMethodName = o.LibShippingMethod.Name
                                
                            }).ToList();
                return query;
            }
        }


        public List<OrderDetailBO> GetOrderDetailsByOrderId(int orderId)
        {
            List<OrderDetailBO> orderDetails = (from od in context.OrderItems
                                                where od.OrderId == orderId
                                                &&(od.Deleted == null|| od.Deleted == false)
                                                select new OrderDetailBO()
                                                {
                                                    OrderItemId = od.OrderItemId
                                                }).ToList();
            return orderDetails;
        }


        public void MarkOrderDetailAsDeleted(int orderDetailId)
        {
            using (var contex = new InThuDoEntities())
            {
                OrderItem orderDetail = contex.OrderItems.Where(od => od.OrderItemId == orderDetailId).FirstOrDefault();
                orderDetail.Deleted = true;
                contex.SaveChanges();
            }
        }
    }
}
