public class TodoPathDto
{
    public string? Name {get; set;}
    public bool? IsComplete {get; set;}
}

/*A razão central de criar essa classe (`TodoPatchDto`) se resume a um problema clássico de tipos de dados em C#: **a pegadinha do valor padrão (`default`)**.

---

**1. O problema com a classe `Todo` original**

Na sua classe original `Todo`, a propriedade `IsComplete` é um tipo de valor primitivo não anulável:

```csharp
public bool IsComplete { get; set; } // O padrão do bool em C# é SEMPRE false

```

Se você usasse `Todo` no endpoint `PATCH` e o usuário enviasse apenas o nome:

```json
{
  "name": "Comprar pão integral"
}

```

O C# inicializa `inputTodo.IsComplete` automaticamente como `false`.

O código não tem como saber se:

* O usuário queria mudar a tarefa para `false`.
* O usuário simplesmente não enviou o campo `IsComplete`.

Ao executar `todo.IsComplete = inputTodo.IsComplete;`, você **desmarcaria acidentalmente** uma tarefa concluída (`true`), corrompendo o dado.

---

**2. A solução com o DTO Anulável (`TodoPatchDto`)**

Para resolver isso, criaram um **DTO (Data Transfer Object)** — um objeto simples feito sob medida apenas para receber a requisição de rede:

```csharp
public class TodoPatchDto
{
    public string? Name { get; set; }
    public bool? IsComplete { get; set; } // bool? aceita true, false ou NULL
}

```

Com `bool?`, os estados possíveis passam a ser três:

| O que veio no JSON | Valor em `inputTodo.IsComplete` | Ação no C# |
| --- | --- | --- |
| `{"isComplete": true}` | `true` | Atualiza para `true` |
| `{"isComplete": false}` | `false` | Atualiza para `false` |
| *Campo omitido* | `null` | **Ignora e mantém o valor atual no banco** |

---

**3. Essa classe tem a ver com a classe `Todo`?**

Elas são **classes separadas com propósitos diferentes**:

* **`Todo` (Entidade de Banco):** Representa a tabela no banco de dados. Os campos refletem como o dado é persistido (`int Id`, `string Name`, `bool IsComplete`, `string? Secret`).
* **`TodoPatchDto` (Contrato de Entrada):** Modela apenas o que o cliente tem permissão de enviar na requisição `PATCH`. Ela não tem `Id`, não tem `Secret` e usa tipos anuláveis (`?`) para permitir atualizações parciais com segurança.

Essa separação garante que o seu banco de dados fique blindado contra alterações indevidas ou substituições acidentais de dados.*/