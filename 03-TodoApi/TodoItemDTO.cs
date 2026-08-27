public class TodoItemDTO
{
    public int Id {get; set;}
    public string? Name {get; set;}
    public bool IsComplete {get; set;}

    public TodoItemDTO() { }

    //public TodoItemDTO(Todo todoItem) => (Id, Name, IsComplete) = (todoItem.Id, todoItem.Name, todoItem.IsComplete);
    //o construtor acima e o debaixo são a mesma coisa, preferi mudar pelo debaixo por ser mais clara
    public TodoItemDTO(Todo todoItem)
    {
     Id = todoItem.Id;
     Name = todoItem.Name;
     IsComplete = todoItem.IsComplete;   
    }
}