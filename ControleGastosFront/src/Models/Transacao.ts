type TransacaoJson = {
  id?: number
  descricao: string
  valor: number
  tipo: number
  categoriaId: number
  pessoaId: number
}

export default class Transacao {
  id?: number
  descricao: string
  valor: number
  tipo: number
  categoriaId: number
  pessoaId: number

  constructor(
    descricao: string,
    valor: number,
    tipo: number,
    categoriaId: number,
    pessoaId: number,
    id?: number
  ) {
    this.id = id
    this.descricao = descricao
    this.valor = valor
    this.tipo = tipo
    this.categoriaId = categoriaId
    this.pessoaId = pessoaId
  }

  static fromJson(json: TransacaoJson): Transacao {
    return new Transacao(
      json.descricao,
      json.valor,
      json.tipo,
      json.categoriaId,
      json.pessoaId,
      json.id
    )
  }

  toJson(): TransacaoJson {
    return {
      id: this.id,
      descricao: this.descricao,
      valor: this.valor,
      tipo: this.tipo,
      categoriaId: this.categoriaId,
      pessoaId: this.pessoaId
    }
  }
}