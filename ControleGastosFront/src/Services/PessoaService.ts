import Config from "../Configs/Config"
import type { ApiResponse } from "../Models/ApiResponse"
import Pessoa from "../Models/Pessoa"

type PessoaJson = {
  id?: number
  nome: string
  idade: number
}

export default class PessoaService {

  static async getPessoas(): Promise<Pessoa[]> {
    const res = await fetch(`${Config.API_URL}/v1/Pessoa`)
    const json: ApiResponse<PessoaJson[]> = await res.json()

    if (!json.success) {
      throw new Error(json.errors.join(", "))
    }

    return json.data.map(x => Pessoa.fromJson(x))
  }

  static async getPessoaById(id: number): Promise<Pessoa> {
    const res = await fetch(`${Config.API_URL}/v1/Pessoa/${id}`)

    const json: ApiResponse<PessoaJson> = await res.json()

    if (!json.success) {
      throw new Error(json.errors.join(", "))
    }

    return Pessoa.fromJson(json.data)
  }

  static async addPessoa(pessoa: Pessoa): Promise<void> {
    const res = await fetch(`${Config.API_URL}/v1/Pessoa`, {
      method: "POST",
      headers: { "Content-Type": "application/json" },
      body: JSON.stringify(pessoa.toJson())
    })

    const json: ApiResponse<null> = await res.json()

    if (!json.success) {
      throw new Error(json.errors.join(", "))
    }
  }

  static async updatePessoa(pessoa: Pessoa): Promise<void> {
    const res = await fetch(`${Config.API_URL}/v1/Pessoa/${pessoa.id}`, {
      method: "PUT",
      headers: { "Content-Type": "application/json" },
      body: JSON.stringify(pessoa.toJson())
    })

    const json: ApiResponse<null> = await res.json()

    if (!json.success) {
      throw new Error(json.errors.join(", "))
    }
  }

  static async deletePessoa(pessoa: Pessoa): Promise<void> {
    const res = await fetch(`${Config.API_URL}/v1/Pessoa/${pessoa.id}`, {
      method: "DELETE",
      headers: { "Content-Type": "application/json" },
      body: JSON.stringify(pessoa.toJson())
    })

    const json: ApiResponse<null> = await res.json()

    if (!json.success) {
      throw new Error(json.errors.join(", "))
    }
  }
}