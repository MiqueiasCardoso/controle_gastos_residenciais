import { useEffect, useState } from "react"
import { useNavigate } from "react-router-dom"


import type Pessoa from "../../Models/Pessoa"
import PessoaService from "../../Services/PessoaService"


function IndexPessoa() {
  const [pessoas, setPessoas] = useState<Pessoa[]>([])
  const [loading, setLoading] = useState(true)

  const navigate = useNavigate()

  useEffect(() => {
    carregarPessoas()
  }, [])

  async function carregarPessoas() {
    try {
      const data = await PessoaService.getPessoas()
      setPessoas(data)
    } catch (err) {
      console.error("Erro ao buscar pessoas", err)
    } finally {
      setLoading(false)
    }
  }

  async function handleDelete(pessoa: Pessoa) {
    const confirmou = window.confirm(
      "Tem certeza que deseja remover essa pessoa e todas as transações?"
    )

    if (!confirmou) return

    try {
      await PessoaService.deletePessoa(pessoa)

      setPessoas(prev => prev.filter(p => p.id !== pessoa.id))

    } catch (err) {
      console.error(err)
      alert(err instanceof Error ? err.message : "Erro ao excluir")
    }
  }

  return (
    <div>
      <h1>Pessoas</h1>

      {/* Botão adicionar */}
      <button
        onClick={() => navigate("/pessoa/add")}
        style={{ marginBottom: "20px" }}
      >
        + Adicionar Pessoa
      </button>

      {/* Loading */}
      {loading && <p>Carregando...</p>}

      {/* Tabela */}
      {!loading && (
        <table style={{ width: "100%", borderCollapse: "collapse" }}>
          <thead>
            <tr>
              <th style={thStyle}>ID</th>
              <th style={thStyle}>Nome</th>
              <th style={thStyle}>Idade</th>
              <th style={thStyle}>Ações</th>
            </tr>
          </thead>

          <tbody>
            {pessoas.map(p => (
              <tr key={p.id}>
                <td style={tdStyle}>{p.id}</td>
                <td style={tdStyle}>{p.nome}</td>
                <td style={tdStyle}>{p.idade}</td>
                <td style={tdStyle}>
                  <button
                    onClick={() => navigate(`/pessoas/edit/${p.id}`)}
                  >
                    Editar
                  </button>
                  &nbsp;
                  <button onClick={() => handleDelete(p)}>
                    Excluir
                  </button>
                </td>
              </tr>
            ))}
          </tbody>
        </table>
      )}

      {/* Sem dados */}
      {!loading && pessoas.length === 0 && (
        <p>Nenhuma pessoa cadastrada</p>
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

export default IndexPessoa