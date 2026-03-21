import { useEffect, useState } from "react"
import TransacaoService from "../../Services/TransacaoService"

type ResumoPessoa = {
  pessoaId: number
  nome: string
  receitas: number
  despesas: number
  saldo: number
}

function RelatorioResumoFinanceiroPorPessoa() {
  const [resumo, setResumo] = useState<ResumoPessoa[]>([])
  const [loading, setLoading] = useState(true)

  useEffect(() => {
    carregarDados()
  }, [])

  async function carregarDados() {
    try {
      const data = await TransacaoService.getResumoPorPessoa()
      setResumo(data)
    } catch (err) {
      console.error("Erro ao carregar relatório", err)
    } finally {
      setLoading(false)
    }
  }

  const totalReceitas = resumo.reduce((sum, x) => sum + x.receitas, 0)
  const totalDespesas = resumo.reduce((sum, x) => sum + x.despesas, 0)
  const saldoTotal = totalReceitas - totalDespesas

  return (
    <div>
      <h1>Total de Receitas e Despesas por Pessoa</h1>

      {loading && <p>Carregando...</p>}

      {!loading && (
        <table style={{ width: "100%", borderCollapse: "collapse" }}>
          <thead>
            <tr>
              <th style={thStyle}>Pessoa</th>
              <th style={thStyle}>Receitas</th>
              <th style={thStyle}>Despesas</th>
              <th style={thStyle}>Saldo</th>
            </tr>
          </thead>

          <tbody>
            {resumo.map((r) => (
              <tr key={r.pessoaId}>
                <td style={tdStyle}>{r.nome}</td>

                <td style={{ ...tdStyle, color: "green" }}>
                  R$ {r.receitas}
                </td>

                <td style={{ ...tdStyle, color: "red" }}>
                  R$ {r.despesas}
                </td>

                <td style={{
                  ...tdStyle,
                  color: r.saldo >= 0 ? "green" : "red"
                }}>
                  R$ {r.saldo}
                </td>
              </tr>
            ))}
          </tbody>

          <tfoot>
            <tr style={{ fontWeight: "bold" }}>
              <td style={tdStyle}>TOTAL</td>

              <td style={{ ...tdStyle, color: "green" }}>
                R$ {totalReceitas}
              </td>

              <td style={{ ...tdStyle, color: "red" }}>
                R$ {totalDespesas}
              </td>

              <td style={{
                ...tdStyle,
                color: saldoTotal >= 0 ? "green" : "red"
              }}>
                R$ {saldoTotal}
              </td>
            </tr>
          </tfoot>
        </table>
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

export default RelatorioResumoFinanceiroPorPessoa