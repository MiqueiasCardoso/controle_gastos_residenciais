# Controle de Gastos Residenciais

Sistema para gerenciamento de gastos residenciais, permitindo o controle de pessoas, categorias e transações financeiras, além da visualização de relatórios consolidados.

---

## 🏗️ Estrutura da Solução

A solução está dividida em dois projetos no backend:

### 🔹 ControleGastos.Api
Projeto responsável pela API:
- Controllers
- Services
- Repositories

### 🔹 ControleGastos.Core
Contém:
- Models
- Enums
- ViewModels

---

## 💻 Front-end

- React + TypeScript
- Estrutura organizada por páginas (Pages), serviços (Services) e models
- Comunicação com a API via `fetch`
---

## ⚙️ Tecnologias utilizadas

- Back-end: .NET 8.0 / C#
- Front-end: React + TypeScript [node v22.12.0]
- Banco de dados: SQLite

---

## 💾 Persistência de dados

O sistema utiliza SQLite como banco de dados.

O arquivo do banco é gerado automaticamente na pasta de publicação da aplicação, permitindo que os dados sejam persistidos mesmo após reinicialização do sistema.

---

## 📌 Funcionalidades

### 👤 Pessoas
- Cadastro, edição, exclusão e listagem
- Ao excluir uma pessoa, todas as transações vinculadas são removidas

---

### 📂 Categorias
- Cadastro e listagem
- Finalidade:
  - Despesa
  - Receita
  - Ambas

---

### 💰 Transações
- Cadastro e listagem

Regras aplicadas:
- Valor deve ser positivo
- Menores de idade (idade < 18) só podem possuir despesas
- A categoria deve ser compatível com o tipo da transação:
  - Despesa → categorias de despesa ou ambas
  - Receita → categorias de receita ou ambas

---

## 📊 Relatórios

### ✔ Por pessoa
- Total de receitas, Total de despesas, Saldo (receitas - despesas), Exibição do total geral ao final.

---

### ✔ Por categoria
- Total de receitas, Total de despesas, Saldo, Exibição do total geral ao final

---

## 🧠 Decisões técnicas

- A lógica de negócio foi centralizada no backend
- O frontend é responsável pela interação e exibição dos dados
- Os relatórios são calculados no backend, retornando dados já consolidados
- Utilização de Dapper para acesso a dados
- Organização em camadas (Controller, Service, Repository)

---

## 🚀 Como executar

### Backend
- Abrir a solução no Visual Studio
- Executar o projeto `ControleGastos.Api`

### Frontend
```bash
npm install
npm run dev

Setar o endereço do servidor da API: ControleGastosFront/src/Configs/Config.ts


