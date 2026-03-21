import { useEffect, useState } from "react"
import { useNavigate } from "react-router-dom"
import Categoria from "../../Models/Categoria"
import CategoriaService from "../../Services/CategoriaService"
import type { FinalidadeCategoria } from "../../Models/Enums/FinalidadeCategoria"


function AddCategoria() {
    const navigate = useNavigate()

    const [descricao, setDescricao] = useState("")
    const [finalidade, setFinalidade] = useState<number>(0)
    const [finalidades, setFinalidades] = useState<FinalidadeCategoria[]>([])

    useEffect(() => {
        // eslint-disable-next-line react-hooks/immutability
        carregarFinalidades()
    }, [])

    async function carregarFinalidades() {
        const data = await CategoriaService.getFinalidades()
        setFinalidades(data)
    }
    async function salvar(e: React.FormEvent) {
        e.preventDefault()

        if (!descricao.trim() || finalidade === 0) {
            alert("Preencha todos os campos")
            return
        }

        try {
            const categoria = new Categoria(descricao, finalidade)

            await CategoriaService.addCategoria(categoria)

            navigate("/categoria")

        } catch (err) {
            console.error("Erro ao salvar categoria", err)

            if (err instanceof Error) {
                alert(err.message)
            } else {
                alert("Erro ao salvar categoria")
            }
        }
    }
    return (
        <div>
            <h1>Adicionar Categoria</h1>

            <form onSubmit={salvar}>

                <div>
                    <label>Descrição</label><br />
                    <input
                        value={descricao}
                        onChange={(e) => setDescricao(e.target.value)}
                    />
                </div>

                <div>
                    <label>Finalidade</label><br />
                    <select
                        value={finalidade}
                        onChange={(e) => setFinalidade(Number(e.target.value))}
                    >
                        <option value={0}>Selecione</option>

                        {finalidades.map(f => (
                            <option key={f.id} value={f.id}>
                                {f.nome}
                            </option>
                        ))}
                    </select>
                </div>

                <button type="submit" style={{ marginTop: "15px" }}>
                    Salvar
                </button>

            </form>
        </div>
    )
}

export default AddCategoria