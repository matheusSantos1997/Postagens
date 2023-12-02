import { Imagem } from './../imagem';

export class PostUpdateDTO {
  id: number;
  titulo: string;
  conteudo: string;
  imagem?: Imagem;

  constructor(id: number, titulo: string, conteudo: string) {
    this.id = id;
    this.titulo = titulo;
    this.conteudo = conteudo;
  }
}
