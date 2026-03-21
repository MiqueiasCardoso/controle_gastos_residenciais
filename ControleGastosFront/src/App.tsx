import { BrowserRouter, Routes, Route } from "react-router-dom"

import Layout from "./Pages/Layout"
import PessoaIndex from "./Pages/Pessoa/Index"
import PessoaAdd from "./Pages/Pessoa/Add"
import PessoaEdit from "./Pages/Pessoa/Edit"


import CategoriasIndex from "./Pages/Categoria/Index"
import CategoriasAdd from "./Pages/Categoria/Add"

import TransacaoIndex from "./Pages/Transacao/Index"
import TransacaoAdd from "./Pages/Transacao/Add"

import RelatorioResumoFinanceiroPorPessoa from "./Pages/Relatorios/ResumoFinaceiroPorPessoa"
import RelatorioResumoFinanceiroPorCategoria from "./Pages/Relatorios/ResumoFinaceiroPorCategoria"

function App() {
  return (
    <BrowserRouter>
      <Routes>
        <Route element={<Layout />}>
          <Route path="/" element={<PessoaIndex />} />
          <Route path="/pessoa" element={<PessoaIndex />} />
          <Route path="/pessoa/add" element={<PessoaAdd />} />
          <Route path="/pessoas/edit/:id" element={<PessoaEdit />} />

          <Route path="/categoria" element={<CategoriasIndex />} />
          <Route path="/categoria/add" element={<CategoriasAdd />} />

          <Route path="/transacao" element={<TransacaoIndex />} />
          <Route path="/transacao/add" element={<TransacaoAdd />} />

          <Route path="/relatorios/resumo-financeiro-pessoa" element={<RelatorioResumoFinanceiroPorPessoa />} />
          <Route path="/relatorios/resumo-financeiro-categoria" element={<RelatorioResumoFinanceiroPorCategoria />} />
        </Route>

      </Routes>
    </BrowserRouter>
  )
}

export default App