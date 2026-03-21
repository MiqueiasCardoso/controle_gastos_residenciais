import { useEffect, useState } from "react"
import { useNavigate, useParams } from "react-router-dom"
import Pessoa from "../../Models/Pessoa"
import PessoaService from "../../Services/PessoaService"


function EditPessoa() {
  const { id } = useParams()
  const navigate = useNavigate()

  const [nome, setNome] = useState("")
  const [idade, setIdade] = useState(0)
  const [loading, setLoading] = useState(true)

  useEffect(() => {
    if (id) carregarPessoa(Number(id))
  }, [id])

  async function carregarPessoa(id: number) {
    try {
      const pessoa = await PessoaService.getPessoaById(id)

      setNome(pessoa.nome)
      setIdade(pessoa.idade)

    } catch (err) {
      console.error("Erro ao carregar pessoa", err)
      alert("Erro ao carregar pessoa")
    } finally {
      setLoading(false)
    }
  }

  async function salvar(e: React.FormEvent) {
    e.preventDefault()

    if (!nome.trim() || idade <= 0) {
      alert("Preencha nome e idade corretamente")
      return
    }

    try {
      const pessoa = new Pessoa(nome, idade, Number(id))

      await PessoaService.updatePessoa(pessoa)

      navigate("/pessoa")

    } catch (err) {
      console.error("Erro ao salvar", err)
      alert(`Erro ao salvar pessoa: ${err}`)
    }
  }

  if (loading) return <p>Carregando...</p>

  return (
    <div>
      <h1>Editar Pessoa</h1>

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

        <button type="submit">Salvar</button>

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

export default EditPessoa