type CategoriaJson = {
  id?: number
  descricao: string
  finalidade: number
}

export default class Categoria {
  id?: number
  descricao: string
  finalidade: number

  constructor(descricao: string, finalidade: number, id?: number) {
    this.id = id
    this.descricao = descricao
    this.finalidade = finalidade
  }

  static fromJson(json: CategoriaJson): Categoria {
    return new Categoria(
      json.descricao,
      json.finalidade,
      json.id
    )
  }

  toJson(): CategoriaJson {
    return {
      id: this.id,
      descricao: this.descricao,
      finalidade: this.finalidade
    }
  }
}