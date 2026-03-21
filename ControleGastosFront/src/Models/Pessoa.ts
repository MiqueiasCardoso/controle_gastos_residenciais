type PessoaJson = {
  id?: number
  nome: string
  idade: number
}

export default class Pessoa {
  id?: number
  nome: string
  idade: number

  constructor(nome: string, idade: number, id?: number) {
    this.id = id
    this.nome = nome
    this.idade = idade
  }

  static fromJson(json: PessoaJson): Pessoa {
    return new Pessoa(
      json.nome,
      json.idade,
      json.id
    )
  }

  toJson(): PessoaJson {
    return {
      id: this.id,
      nome: this.nome,
      idade: this.idade
    }
  }
}