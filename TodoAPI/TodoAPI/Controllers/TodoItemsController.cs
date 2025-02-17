using Microsoft.AspNetCore.Mvc;
using TodoApi.Models;
using Microsoft.AspNetCore.Authorization;
using TodoAPI.Definitions;

namespace TodoApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class TodoItemsController : ControllerBase
    {
        private readonly ITodoItemService _todoItemService;

        public TodoItemsController(ITodoItemService todoItemService)
        {
            _todoItemService = todoItemService;
        }

        // GET: api/TodoItems
        [HttpGet]
        public async Task<ActionResult<IEnumerable<TodoItem>>> GetTodoItems()
        {
            try
            {
                var todoItems = await _todoItemService.GetTodoItems();
                if (todoItems.Count < 1)
                {
                    return NotFound();
                }
                return todoItems;
            }catch (Exception ex)
            {
                throw;
            }
          
        }

        // GET: api/TodoItems/5
        [HttpGet("{id}")]
        public async Task<ActionResult<TodoItem>> GetTodoItem(decimal id)
        {
            try
            {
                var todoItem = await _todoItemService.GetTodoItem(id);
                if (todoItem.Name == null)
                {
                    return NotFound();
                }

                return todoItem;
            }catch (Exception ex)
            {
                throw;
            }
            
        }

        // PUT: api/TodoItems/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutTodoItem(decimal id, TodoItem todoItem)
        {
            if (id != todoItem.Id)
            {
                return BadRequest();
            }

            try
            {
                var item = await _todoItemService.GetTodoItem(id);
                if (item.Name == null)
                {
                    return NotFound();
                }
                await _todoItemService.UpdateTodoItem(todoItem);
            }
            catch (Exception ex)
            {
                throw;
            }

            return NoContent();
        }

        // POST: api/TodoItems
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<TodoItem>> PostTodoItem(TodoItem todoItem)
        {
            try
            {
                await _todoItemService.CreateTodoItem(todoItem);
            }catch(Exception ex) 
            { 
                throw; 
            }

            return CreatedAtAction(nameof(GetTodoItem), new { id = todoItem.Id }, todoItem);
        }

        // DELETE: api/TodoItems/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteTodoItem(long id)
        {
            try
            {
                var todoItem = await _todoItemService.GetTodoItem(id);
                if (todoItem.Name == null)
                {
                    return NotFound();
                }

                await _todoItemService.DeleteTodoItem(id);
            }catch (Exception ex)
            {
                throw;
            }
            
            return NoContent();
        }
       
    }
}
