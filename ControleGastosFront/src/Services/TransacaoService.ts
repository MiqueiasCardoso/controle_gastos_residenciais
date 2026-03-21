import Config from "../Configs/Config"
import type { ApiResponse } from "../Models/ApiResponse"
import Transacao from "../Models/Transacao"

type TransacaoJson = {
  id?: number
  descricao: string
  valor: number
  tipo: number
  categoriaId: number
  pessoaId: number
}

type ResumoPorPessoa = {
  pessoaId: number
  nome: string
  receitas: number
  despesas: number
  saldo: number
}

type ResumoPorCategoria = {
  categoriaId: number
  descricao: string
  receitas: number
  despesas: number
  saldo: number
}

export default class TransacaoService {

  static async getTransacoes(): Promise<Transacao[]> {
    const res = await fetch(`${Config.API_URL}/v1/Transacao`)
    const json: ApiResponse<TransacaoJson[]> = await res.json()

    if (!json.success) {
      throw new Error(json.errors.join(", "))
    }

    return json.data.map(x => Transacao.fromJson(x))
  }

  static async addTransacao(transacao: Transacao): Promise<void> {
    const res = await fetch(`${Config.API_URL}/v1/Transacao`, {
      method: "POST",
      headers: {
        "Content-Type": "application/json"
      },
      body: JSON.stringify(transacao.toJson())
    })

    const json: ApiResponse<null> = await res.json()

    if (!json.success) {
      throw new Error(json.errors.join(", "))
    }
  }

  static async getResumoPorPessoa(): Promise<ResumoPorPessoa[]> {
    const res = await fetch(`${Config.API_URL}/v1/Transacao/ResumoFinanceiroPorPessoa`)
    const json: ApiResponse<ResumoPorPessoa[]> = await res.json()

    if (!json.success) {
      throw new Error(json.errors.join(", "))
    }

    return json.data
  }
  static async getResumoPorCategoria(): Promise<ResumoPorCategoria[]> {
    const res = await fetch(`${Config.API_URL}/v1/Transacao/ResumoFinanceiroPorCategoria`)
    const json: ApiResponse<ResumoPorCategoria[]> = await res.json()

    if (!json.success) {
      throw new Error(json.errors.join(", "))
    }

    return json.data
  }

}