import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { take } from 'rxjs/operators';
import { Observable } from 'rxjs';
import { environment } from 'src/environments/environment';
import { Imagem } from '../models/imagem';

@Injectable({
  providedIn: 'root'
})
export class ImagemService {

  URL: string = environment.ApiUrl;

  constructor(private http: HttpClient) { }

  getImagemById(id: number): Observable<Imagem> {
     const apiUrl = `${this.URL}/Imagens/GetImagensById/${id}`;
     return this.http.get<Imagem>(apiUrl).pipe(take(1));
  }

  uploadImagem(postId: number, file: FormData): Observable<Imagem> {
      const apiUrl = `${this.URL}/Imagens/UploadImagem/${postId}`;
      return this.http.post<Imagem>(apiUrl, file);
  }

  putImagem(id: number, file: FormData): Observable<Imagem> {
    const apiUrl = `${this.URL}/Imagens/UpdateImagem/${id}`;
    return this.http.put<Imagem>(apiUrl, file);
  }

  deleteImagem(id: number): Observable<Imagem> {
    const apiUrl = `${this.URL}/Imagens/DeleteImagem/${id}`;
    return this.http.delete<Imagem>(apiUrl);
  }


}
