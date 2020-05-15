using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
//using TodoApi.Models;

namespace TodoApi
{
    [ApiController]
    [Route("[controller]")]
    public class myController: ControllerBase
    {
        
        private readonly OrderContext todoDb;
        //我的测试get
        // [HttpGet("{test}")]
        // public void printing(string test)
        // {
        //     Console.WriteLine(test);
        // }

        //构造函数把TodoContext 作为参数，Asp.net core 框架可以自动注入TodoContext对象
        public myController(OrderContext context)
        {
            this.todoDb = context;
        }
        [HttpGet("{test}")]
        public void myprint(string test){
            Console.WriteLine("test:"+test);
            Console.WriteLine("todoDb");
        }
    }
    [ApiController]
    [Route("api/[controller]")]
    public class TodoController : ControllerBase
    {

        private readonly OrderContext todoDb;
        //我的测试get
        // [HttpGet("{test}")]
        // public void printing(string test)
        // {
        //     Console.WriteLine(test);
        // }

        //构造函数把TodoContext 作为参数，Asp.net core 框架可以自动注入TodoContext对象
        public TodoController(OrderContext context)
        {
            this.todoDb = context;
        }

        // GET: api/todo/{id}  id为路径参数
        [HttpGet("{id}")]
        public ActionResult<Order> GetOrderItem(long id)
        {
            var todoItem = todoDb.Orders.FirstOrDefault(t => t.OrderId == id);
            if (todoItem == null)
            {
                return NotFound();
            }
            return todoItem;
        }

        // GET: api/todo
        // GET: api/todo/pageQuery?name=课程&&isComplete=true
        [HttpGet]
        public ActionResult<List<Order>> GetOrderItems(string name, bool? isComplete)
        {
            var query = buildQuery(name, isComplete);
            return query.ToList();
        }

        // GET: api/todo/pageQuery?skip=5&&take=10  
        // GET: api/todo/pageQuery?name=课程&&isComplete=true&&skip=5&&take=10
        [HttpGet("pageQuery")]
        public ActionResult<List<Order>> queryOrderItem(string name, bool? isComplete, int skip, int take)
        {
            var query = buildQuery(name, isComplete).Skip(skip).Take(take);
            return query.ToList();
        }

        private IQueryable<Order> buildQuery(string name,bool? isComplete)
        {
            IQueryable<Order> query = todoDb.Orders;
            if (name != null)
            {
                query = query.Where(t => t.Buyer.Contains(name));
            }
            Console.WriteLine("iscomplet="+isComplete);
            return query;

        }


        // POST: api/todo
        [HttpPost]
        public ActionResult<Order> PostOrderItem(Order todo)
        {
            try
            {
                todoDb.Orders.Add(todo);
                todoDb.SaveChanges();
            }
            catch (Exception e)
            {
                return BadRequest(e.InnerException.Message);
            }
            return todo;
        }
        //增加订单详情项
        [HttpPost]
        public ActionResult<Order> AddOrderItem(int orderId,OrderItem orderItem){
            IQueryable<Order> query = todoDb.Orders;
            if(query.Where(t=>t.OrderId==orderId).ToList()!=null){
                orderItem.OrderId=orderId;
                todoDb.OrderItems.Add(orderItem);
                todoDb.SaveChanges();
                
            }
            return query.Where(t=>t.OrderId==orderId).ToList()[0];
        }
        // PUT: api/todo/{id}
        [HttpPut("{id}")]
        public ActionResult<Order> PutOrderItem(long id, Order todo)
        {
            if (id != todo.OrderId)
            {
                return BadRequest("Id cannot be modified!");
            }
            try
            {
                todoDb.Entry(todo).State = EntityState.Modified;
                todoDb.SaveChanges();
            }
            catch (Exception e)
            {
                string error = e.Message;
                if (e.InnerException != null) error = e.InnerException.Message;
                return BadRequest(error);
            }
            return NoContent();
        }

        // DELETE: api/todo/{id}
        [HttpDelete("{id}")]
        public ActionResult DeleteOrderItem(long id)
        {
            try
            {
                var todo = todoDb.Orders.FirstOrDefault(t => t.OrderId == id);
                if (todo != null)
                {
                    todoDb.Remove(todo);
                    todoDb.SaveChanges();
                }
            }
            catch (Exception e)
            {
                return BadRequest(e.InnerException.Message);
            }
            return NoContent();
        }

    }
}