# CashSmart

Seu gerenciador financeiro inteligente, onde você cadastra suas receitas e despesas e a inteligência artificial, analisa seus gastos.

![image](https://github.com/user-attachments/assets/5cbd3f10-9700-479a-a24f-9f85ca7c35aa)


# Introdução 
O Smart Cash é uma plataforma de gestão financeira pessoal desenvolvida para auxiliar os 
usuários a monitorarem suas receitas e despesas, calcularem seu saldo mensal ou anual e 
obterem insights financeiros. A ferramenta visa facilitar o planejamento 
financeiro, oferecendo relatórios intuitivos e sugestões de economia baseadas em 
análises de padrões de gastos. 

# Tecnologias Utilizadas

## Linguagens
* C# (Backend - .NET Core) 
* TypeScript (Frontend - React) 

## Frameworks e Bibliotecas 
* .NET Core (Desenvolvimento da API) 
* React (Interface do usuário) 
* React Bootstrap (Estilização da interface) 
* Axios (Consumo da API)
* Apexcharts (Gráficos)
* JWT (JSON Web Token) – Autenticação segura baseada em tokens
* BCrypt – Criptografia de senhas dos usuários no banco de dados

# Banco de dados

Banco de dados utilizado foi o SQLServer, modelo de Entidade Relacionamento: 

![SmartCash (1)](https://github.com/user-attachments/assets/d6ac6c84-b55f-44b3-af9e-28e97cf526a5)

## 📊 Estrutura das Tabelas

---

### 🧑‍💼 Usuários

| Coluna           | Tipo       | Observações              |
|------------------|------------|---------------------------|
| UsuarioID        | `int`      | PK (chave primária)       |
| Nome             | `string`   |                           |
| Email            | `string`   |                           |
| Senha            | `string`   |                           |
| ativo            | `bool`     | Indica se o usuário está ativo |
| dataCriacao      | `datetime` | Data de criação do registro |
| dataAtualizacao  | `datetime` | Última atualização        |

---

### 💸 Transacoes

| Coluna            | Tipo       | Observações                        |
|-------------------|------------|-------------------------------------|
| id                | `int`      | PK (chave primária)                |
| valor             | `decimal`  | Valor da transação                 |
| dataHora          | `datetime` | Quando ocorreu a transação         |
| dataCriacao       | `datetime` | Quando foi registrada              |
| UsuarioID         | `int`      | FK → `Usuários(UsuarioID)`         |
| CategoriaID       | `int`      | FK → `Categorias(id)`              |
| FormaPagamentoID  | `int`      | FK → `FormasPagamento(id)`         |

---

### 🏷️ Categorias

| Coluna           | Tipo       | Observações              |
|------------------|------------|---------------------------|
| id               | `int`      | PK                        |
| nome             | `string`   | Nome da categoria         |
| dataCriacao      | `datetime` |                           |
| dataAtualizacao  | `datetime` |                           |

---

### 💳 FormasPagamento

| Coluna           | Tipo       | Observações                      |
|------------------|------------|-----------------------------------|
| id               | `int`      | PK                                |
| nome             | `string`   | Ex: Cartão, PIX, Boleto           |
| dataCriacao      | `datetime` |                                   |
| dataAtualizacao  | `datetime` |                                   |


## ⚙️ Stored Procedures

As Stored Procedures abaixo foram criadas para facilitar operações comuns do sistema, como exclusão segura de dados e geração de relatórios consolidados. Elas ajudam a manter a lógica de negócios centralizada no banco de dados, com transações garantindo integridade e rollback automático em caso de falhas. Você pode ver elas no arquivo StoredProcedures.sql

### 🧽 `SP_DELETAR_FORMA_PAGAMENTO_E_TRANSACOES`

**Descrição:**  
Remove uma forma de pagamento e todas as transações vinculadas a ela para um determinado usuário.  
Utiliza **transações SQL (`BEGIN TRANSACTION`...`COMMIT` / `ROLLBACK`)** para garantir que nenhuma transação fique órfã ou dados sejam parcialmente excluídos.

---

### 📊 `SP_INFORMACOES_ANUAIS_POR_MESES_TRANSACOES`

**Descrição:**  
Gera um relatório anual detalhado por mês com as seguintes informações financeiras de um usuário

essa procedure permite construir dashboards e gráficos analíticos com base nas movimentações mensais do usuário.

**Observações:**
- O agrupamento é feito pelo nome do mês (`DATENAME(MONTH, Data)`).
- Pode ser utilizada para alimentar gráficos de linha, barras e visões comparativas de ganhos x gastos ao longo do ano.


# 🏗️ Arquitetura do Sistema

O sistema segue uma arquitetura em camadas, promovendo organização, escalabilidade e manutenção facilitada:

- **Camada de Apresentação:**  
  Interface com o usuário (Frontend web) e endpoints da API.

- **Camada de Aplicação:**  
  Coordena as operações entre as camadas, controlando fluxos e acessos.

- **Camada de Negócio:**  
  Contém as regras de negócio e validações da lógica do domínio.

- **Camada de Dados:**  
  Responsável por persistência e consultas no banco de dados (SQL Server).

- **Camada de Serviços:**  
  Integrações com serviços externos (ex: envio de email, APIs financeiras).


# 🎨 Interfaces

A seguir, são apresentadas as principais telas desenvolvidas na aplicação **Smart Cash**, proporcionando uma experiência intuitiva e acessível ao usuário.

---

### 🔐 Login  
Tela de autenticação para acesso ao sistema.

<img src="https://github.com/user-attachments/assets/ecee3396-9655-4a80-85f4-39630a6ac6ed" width="700"/>

---

### 📝 Cadastro  
Formulário de criação de conta para novos usuários.

<img src="https://github.com/user-attachments/assets/475b1de5-2b72-4b37-90b5-da6a6dc3c0e5" width="700"/>

---

### 📊 Relatórios  
Visualização de receitas, despesas e saldo mensal.

<img src="https://github.com/user-attachments/assets/c332098f-4bd9-466f-9d1a-07e18a10b6b7" width="700"/>

---

### 🤖 Relatório IA  
Sugestões inteligentes com base nos gastos do usuário.

<img src="https://github.com/user-attachments/assets/347e015b-c60e-4cf9-bbe8-602c29514371" width="400"/>

---

### 🗂️ Cadastro de Categorias  
Gerenciamento de categorias de transações financeiras.

<img src="https://github.com/user-attachments/assets/0414c086-9d35-4e33-900e-7166e24a7cd1" width="700"/>

---

### Reponsividade  
Tela com informações e atribuições do responsável pelo projeto.

<img src="https://github.com/user-attachments/assets/06e680c4-f5d3-430a-84a3-c6906502d1e5" width="400"/>


# 🔮 Melhorias Futuras
Integração com IA Avançada: Substituir a IA atual por modelos mais robustos e precisos para oferecer análises financeiras mais inteligentes e personalizadas, com base no perfil de consumo do usuário.

Suporte a Parcelamentos: Implementar o controle de transações parceladas, permitindo ao usuário visualizar parcelas futuras e acompanhar o impacto no orçamento mensal.



