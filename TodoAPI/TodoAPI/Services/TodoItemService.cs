using TodoApi.Models;
using TodoAPI.Definitions;

namespace TodoAPI.Services
{
    public class TodoItemService : ITodoItemService
    {
        private readonly ITodoItemRepository _repository;

        public TodoItemService(ITodoItemRepository todoItemRepository)
        {
            _repository = todoItemRepository;
        }
        public async Task<bool> DeleteTodoItem(decimal id)
        {
            return await _repository.DeleteTodoItem(id);
        }

        public async Task<TodoItem> GetTodoItem(decimal id)
        {
            return await _repository.GetTodoItem(id);
        }

        public async Task<List<TodoItem>> GetTodoItems()
        {
            return await _repository.GetTodoItems();
        }

        public async Task<bool> CreateTodoItem(TodoItem item)
        {
            return await _repository.CreateTodoItem(item);
        }

        public async Task<bool> UpdateTodoItem(TodoItem item)
        {
            return await _repository.UpdateTodoItem(item);
        }
    }
}
