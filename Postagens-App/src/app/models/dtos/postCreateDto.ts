export class PostCreateDTO {
  titulo: string;
  conteudo: string;

  constructor(titulo: string, conteudo: string) {
    this.titulo = titulo;
    this.conteudo = conteudo;
  }
}
