import Config from "../Configs/Config"
import type { ApiResponse } from "../Models/ApiResponse"
import Categoria from "../Models/Categoria"
import type { FinalidadeCategoria } from "../Models/Enums/FinalidadeCategoria"


type CategoriaJson = {
  id?: number
  descricao: string
  finalidade: number
}

export default class CategoriaService {

  static async getCategorias(): Promise<Categoria[]> {
    const res = await fetch(`${Config.API_URL}/v1/Categoria`)
    const json: ApiResponse<CategoriaJson[]> = await res.json()

    if (!json.success) {
      throw new Error(json.errors.join(", "))
    }

    return json.data.map(x => Categoria.fromJson(x))
  }

  static async getFinalidades(): Promise<FinalidadeCategoria[]> {
    const res = await fetch(`${Config.API_URL}/v1/finalidade-categoria`)
    const json: ApiResponse<FinalidadeCategoria[]> = await res.json()

    if (!json.success) {
      throw new Error(json.errors.join(", "))
    }

    return json.data
  }

  static async addCategoria(categoria: Categoria): Promise<void> {
    const res = await fetch(`${Config.API_URL}/v1/Categoria`, {
      method: "POST",
      headers: {
        "Content-Type": "application/json"
      },
      body: JSON.stringify(categoria.toJson())
    })

    const json: ApiResponse<null> = await res.json()

    if (!json.success) {
      throw new Error(json.errors.join(", "))
    }
  }
}