import Menu from "./Menu"
import { Outlet } from "react-router-dom"

function Layout() {
  return (
    <div style={{
      display: "flex",
      height: "100vh",
      background: "#f5f6fa"
    }}>

      {/* Sidebar */}
      <div style={{
        width: "240px",
        background: "#2f3640",
        color: "#fff",
        padding: "20px",
        borderTopRightRadius: "20px",
        borderBottomRightRadius: "20px",
        boxShadow: "2px 0 10px rgba(0,0,0,0.1)"
      }}>
        <h2 style={{ marginBottom: "30px" }}>Controle de Gastos</h2>
        <Menu />
      </div>

      {/* Conteúdo */}
      <div style={{
        flex: 1,
        padding: "30px"
      }}>
        <div style={{
          background: "#fff",
          borderRadius: "16px",
          padding: "20px",
          boxShadow: "0 4px 15px rgba(0,0,0,0.05)",
          minHeight: "100%"
        }}>
          <Outlet />
        </div>
      </div>

    </div>
  )
}

export default Layout