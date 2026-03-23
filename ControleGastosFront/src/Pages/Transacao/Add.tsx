import { useEffect, useState } from "react"
import { useNavigate } from "react-router-dom"

import Transacao from "../../Models/Transacao"
import CategoriaService from "../../Services/CategoriaService"
import PessoaService from "../../Services/PessoaService"
import type Categoria from "../../Models/Categoria"
import type Pessoa from "../../Models/Pessoa"
import TransacaoService from "../../Services/TransacaoService"


function AddTransacao() {
  const navigate = useNavigate()

  const [descricao, setDescricao] = useState("")
  const [valor, setValor] = useState(0)
  const [tipo, setTipo] = useState(1)
  const [categoriaId, setCategoriaId] = useState(0)
  const [pessoaId, setPessoaId] = useState(0)

  const [categorias, setCategorias] = useState<Categoria[]>([])
  const [pessoas, setPessoas] = useState<Pessoa[]>([])

  const [loading, setLoading] = useState(false)

  useEffect(() => {
    carregarDados()
  }, [])

  async function carregarDados() {
    const [cats, pess] = await Promise.all([
      CategoriaService.getCategorias(),
      PessoaService.getPessoas()
    ])

    setCategorias(cats)
    setPessoas(pess)
  }

  const categoriasFiltradas = categorias.filter(c =>
    c.finalidade === tipo || c.finalidade === 3
  )

  useEffect(() => {
    const pessoa = pessoas.find(p => p.id === pessoaId)

    if (pessoa && pessoa.idade < 18) {
      setTipo(1) // força despesa
    }
  }, [pessoaId])

  async function salvar(e: React.FormEvent) {
    e.preventDefault()

    if (!descricao.trim() || valor <= 0 || !categoriaId || !pessoaId) {
      alert("Preencha todos os campos corretamente")
      return
    }

    setLoading(true)

    try {
      const transacao = new Transacao(
        descricao,
        valor,
        tipo,
        categoriaId,
        pessoaId
      )

      await TransacaoService.addTransacao(transacao)

      navigate("/transacao")

    } catch (err) {
      console.error(err)
      alert(err instanceof Error ? err.message : "Erro ao salvar")
    } finally {
      setLoading(false)
    }
  }

  return (
    <div>
      <h1>Adicionar Transação</h1>

      <form onSubmit={salvar} style={{ display: "flex", flexDirection: "column", gap: "15px" }}>

        <input
          placeholder="Descrição"
          value={descricao}
          onChange={(e) => setDescricao(e.target.value)}
        />

        <input
          type="number"
          placeholder="Valor"
          value={valor}
          onChange={(e) => setValor(Number(e.target.value))}
        />

        {/* Tipo */}
        <select value={tipo} onChange={(e) => setTipo(Number(e.target.value))}>
          <option value={1}>Despesa</option>
          <option value={2}>Receita</option>
        </select>

        {/* Pessoa */}
        <select value={pessoaId} onChange={(e) => setPessoaId(Number(e.target.value))}>
          <option value={0}>Selecione a pessoa</option>
          {pessoas.map(p => (
            <option key={p.id} value={p.id}>
              {p.nome}
            </option>
          ))}
        </select>
        
        {/* Categoria */}
        <select value={categoriaId} onChange={(e) => setCategoriaId(Number(e.target.value))}>
          <option value={0}>Selecione a categoria</option>

          {categorias.map((c) => (
            <option key={c.id} value={c.id}>
              {c.descricao}
            </option>
          ))}
        </select>

        <button type="submit" disabled={loading}>
          {loading ? "Salvando..." : "Salvar"}
        </button>

      </form>
    </div>
  )
}

export default AddTransacao
