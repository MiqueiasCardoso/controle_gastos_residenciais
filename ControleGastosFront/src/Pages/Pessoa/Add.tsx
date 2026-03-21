import { useState } from "react"
import { useNavigate } from "react-router-dom"
import Pessoa from "../../Models/Pessoa"
import PessoaService from "../../Services/PessoaService"


function AddPessoa() {
  const navigate = useNavigate()

  const [nome, setNome] = useState("")
  const [idade, setIdade] = useState(0)
  const [loading, setLoading] = useState(false)

  async function salvar(e: React.FormEvent) {
  e.preventDefault()

  if (!nome.trim() || idade <= 0) {
    alert("É Necessário preencher todos os campos")
    return
  }

  setLoading(true)

  try {
    const pessoa = new Pessoa(nome, idade)

    await PessoaService.addPessoa(pessoa)

    navigate("/pessoa") 

  } catch (err) {
    console.error("Erro ao salvar", err)
    alert(`Erro ao salvar pessoa: ${err}`)
  } finally {
    setLoading(false)
  }
}

  return (
    <div>
      <h1>Adicionar Pessoa</h1>

      <form onSubmit={salvar}>
        
        <div style={{ marginBottom: "10px" }}>
          <label>Nome</label><br />
          <input
            value={nome}
            onChange={(e) => setNome(e.target.value)}
          />
        </div>

        <div style={{ marginBottom: "10px" }}>
          <label>Idade</label><br />
          <input
            type="number"
            value={idade}
            onChange={(e) => setIdade(Number(e.target.value))}
          />
        </div>

        <button type="submit" disabled={loading}>
          {loading ? "Salvando..." : "Salvar"}
        </button>

        <button
          type="button"
          onClick={() => navigate("/pessoa")}
          style={{ marginLeft: "10px" }}
        >
          Cancelar
        </button>

      </form>
    </div>
  )
}

export default AddPessoa