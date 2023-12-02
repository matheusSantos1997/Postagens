import { Imagem } from "./imagem";

export class Post {
   id: number;
   titulo: string;
   conteudo: string;
   usuarioId?: number;
   imagem?: Imagem;
}
