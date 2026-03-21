import { NavLink } from "react-router-dom"

function Menu() {
  const linkStyle = {
    padding: "10px 15px",
    borderRadius: "10px",
    textDecoration: "none",
    color: "#dcdde1"
  }

  return (
    <div style={{ display: "flex", flexDirection: "column", gap: "10px" }}>
      
      <NavLink
        to="/pessoa"
        style={({ isActive }) => ({
          ...linkStyle,
          background: isActive ? "#40739e" : "transparent"
        })}
      >
        Pessoas
      </NavLink>

      <NavLink
        to="/categoria"
        style={({ isActive }) => ({
          ...linkStyle,
          background: isActive ? "#40739e" : "transparent"
        })}
      >
        Categorias
      </NavLink>

      <NavLink
        to="/transacao"
        style={({ isActive }) => ({
          ...linkStyle,
          background: isActive ? "#40739e" : "transparent"
        })}
      >
        Transações
      </NavLink>


        <NavLink
        to="/relatorios/resumo-financeiro-pessoa"
        style={({ isActive }) => ({
          ...linkStyle,
          background: isActive ? "#40739e" : "transparent"
        })}
      >
        Total por Pessoa
      </NavLink>

        <NavLink
        to="/relatorios/resumo-financeiro-categoria"
        style={({ isActive }) => ({
          ...linkStyle,
          background: isActive ? "#40739e" : "transparent"
        })}
      >
        Total por Categoria
      </NavLink>

      

    </div>
  )
}

export default Menu