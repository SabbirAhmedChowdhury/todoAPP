using TodoApi.Models;

namespace TodoAPI.Definitions
{
    public interface ITodoItemService
    {
        public Task<TodoItem> GetTodoItem(decimal id);
        public Task<List<TodoItem>> GetTodoItems();
        public Task<bool> CreateTodoItem(TodoItem item);
        public Task<bool> DeleteTodoItem(decimal id);
        public Task<bool> UpdateTodoItem(TodoItem item);
    }
}
