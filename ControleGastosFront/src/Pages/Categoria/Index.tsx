import { useEffect, useState } from "react"
import { useNavigate } from "react-router-dom"
import Categoria from "../../Models/Categoria"
import CategoriaService from "../../Services/CategoriaService"
import type { FinalidadeCategoria } from "../../Models/Enums/FinalidadeCategoria"


function IndexCategoria() {
    const [categorias, setCategorias] = useState<Categoria[]>([])
    const [finalidades, setFinalidades] = useState<FinalidadeCategoria[]>([])
    const [loading, setLoading] = useState(true)

    const navigate = useNavigate()

    useEffect(() => {
        carregarDados()
    }, [])

    async function carregarDados() {
        try {
            const [cats, fins] = await Promise.all([
                CategoriaService.getCategorias(),
                CategoriaService.getFinalidades()
            ])

            setCategorias(cats)
            setFinalidades(fins)

        } catch (err) {
            console.error("Erro ao carregar categorias", err)
        } finally {
            setLoading(false)
        }
    }

    function getNomeFinalidade(id: number) {
        const f = finalidades.find(x => x.id === id)
        return f ? f.nome : "-"
    }

    return (
        <div>
            <h1>Categorias</h1>

            <button
                onClick={() => navigate("/categoria/add")}
                style={{ marginBottom: "20px" }}
            >
                + Adicionar Categoria
            </button>

            {loading && <p>Carregando...</p>}

            {!loading && (
                <table style={{ width: "100%", borderCollapse: "collapse" }}>
                    <thead>
                        <tr>
                            <th style={thStyle}>ID</th>
                            <th style={thStyle}>Descrição</th>
                            <th style={thStyle}>Finalidade</th>

                        </tr>
                    </thead>

                    <tbody>
                        {categorias.map(c => (
                            <tr key={c.id}>
                                <td style={tdStyle}>{c.id}</td>
                                <td style={tdStyle}>{c.descricao}</td>
                                <td style={tdStyle}>{getNomeFinalidade(c.finalidade)}</td>
                            </tr>
                        ))}
                    </tbody>
                </table>
            )}

            {!loading && categorias.length === 0 && (
                <p>Nenhuma categoria cadastrada</p>
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

export default IndexCategoria