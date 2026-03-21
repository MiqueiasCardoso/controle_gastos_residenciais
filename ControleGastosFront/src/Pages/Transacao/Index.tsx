import { useEffect, useState } from "react"
import { useNavigate } from "react-router-dom"



import type Transacao from "../../Models/Transacao"
import TransacaoService from "../../Services/TransacaoService"
import CategoriaService from "../../Services/CategoriaService"
import PessoaService from "../../Services/PessoaService"
import type Pessoa from "../../Models/Pessoa"
import type Categoria from "../../Models/Categoria"

function IndexTransacao() {
  const [transacoes, setTransacoes] = useState<Transacao[]>([])
  const [categorias, setCategorias] = useState<Categoria[]>([])
  const [pessoas, setPessoas] = useState<Pessoa[]>([])
  const [loading, setLoading] = useState(true)

  const navigate = useNavigate()

  useEffect(() => {
    carregarDados()
  }, [])

  async function carregarDados() {
    try {
      const [trans, cats, pess] = await Promise.all([
        TransacaoService.getTransacoes(),
        CategoriaService.getCategorias(),
        PessoaService.getPessoas()
      ])

      setTransacoes(trans)
      setCategorias(cats)
      setPessoas(pess)

    } catch (err) {
      console.error("Erro ao carregar transações", err)
    } finally {
      setLoading(false)
    }
  }

  function getNomePessoa(id: number) {
    return pessoas.find(p => p.id === id)?.nome || "-"
  }

  function getNomeCategoria(id: number) {
    return categorias.find(c => c.id === id)?.descricao || "-"
  }

  function getTipoNome(tipo: number) {
    return tipo === 1 ? "Despesa" : "Receita"
  }

  function getCorValor(tipo: number) {
    return tipo === 1 ? "red" : "green"
  }

  return (
    <div>
      <h1>Transações</h1>

      <button
        onClick={() => navigate("/transacao/add")}
        style={{ marginBottom: "20px" }}
      >
        + Nova Transação
      </button>

      {loading && <p>Carregando...</p>}

      {!loading && (
        <table style={{ width: "100%", borderCollapse: "collapse" }}>
          <thead>
            <tr>
              <th style={thStyle}>ID</th>
              <th style={thStyle}>Descrição</th>
              <th style={thStyle}>Valor</th>
              <th style={thStyle}>Tipo</th>
              <th style={thStyle}>Categoria</th>
              <th style={thStyle}>Pessoa</th>
            </tr>
          </thead>

          <tbody>
            {transacoes.map(t => (
              <tr key={t.id}>
                <td style={tdStyle}>{t.id}</td>
                <td style={tdStyle}>{t.descricao}</td>

                <td style={{ ...tdStyle, color: getCorValor(t.tipo) }}>
                  {t.tipo === 1 ? "-" : "+"} R$ {t.valor}
                </td>

                <td style={tdStyle}>{getTipoNome(t.tipo)}</td>
                <td style={tdStyle}>{getNomeCategoria(t.categoriaId)}</td>
                <td style={tdStyle}>{getNomePessoa(t.pessoaId)}</td>
              </tr>
            ))}
          </tbody>
        </table>
      )}

      {!loading && transacoes.length === 0 && (
        <p>Nenhuma transação cadastrada</p>
      )}
    </div>
  )
}

const thStyle = {
  borderBottom: "1px solid #ccc",
  textAlign: "left" as const,
  padding: "10px"
}

const tdStyle = {
  padding: "10px",
  borderBottom: "1px solid #eee"
}

export default IndexTransacao