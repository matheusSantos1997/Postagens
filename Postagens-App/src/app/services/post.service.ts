import { HttpClient, HttpParams } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { environment } from 'src/environments/environment';
import { Post } from '../models/post';
import { PostCreateDTO } from '../models/dtos/postCreateDto';
import { PostUpdateDTO } from '../models/dtos/postUpdateDto';
import { map, take } from 'rxjs/operators';
import { PaginatedResult } from '../models/pagination/paginatedResult';

@Injectable({
  providedIn: 'root'
})
export class PostService {

  URL: string = environment.ApiUrl;

  constructor(private http: HttpClient) { }

  /*getAllPosts(): Observable<Post[]> {
     const urlApi = `${this.URL}/Posts/PegarTodosPosts`;
     return this.http.get<Post[]>(urlApi).pipe(take(1));
  }*/
  getAllPosts(page?: number, itemsPerPage?: number): Observable<PaginatedResult<Post[]>> {

    const paginatedResult: PaginatedResult<Post[]> = new PaginatedResult<Post[]>();

    let params = new HttpParams;

    if(page !== null && itemsPerPage !== null) {
      params = params.append('pageNumber', page.toString());
      params = params.append('pageSize', itemsPerPage.toString())
    }

    const urlApi = `${this.URL}/Posts/PegarTodosPosts`;

    return this.http.get<Post[]>(urlApi, { observe: 'response', params })
                    .pipe(
                      take(1),
                      map((response) => {
                        paginatedResult.result = response.body;
                        if(response.headers.has('Pagination')) {
                          paginatedResult.pagination = JSON.parse(response.headers.get('Pagination'));
                        }

                        return paginatedResult;
                      }));
  }


  getPostById(id: number): Observable<Post> {
    const urlApi = `${this.URL}/Posts/ListarPostPorId/${id}`;
    return this.http.get<Post>(urlApi).pipe(take(1));
  }

  createNewPost(post: PostCreateDTO): Observable<Post> {
     const urlApi = `${this.URL}/Posts/CadastrarNovoPost`;
     return this.http.post<Post>(urlApi, post);
  }

  updatePost(id: number, post: PostUpdateDTO): Observable<Post> {
     const urlApi = `${this.URL}/Posts/AtualizarPost/${id}`;
     return this.http.put<Post>(urlApi, post);
  }

  deletePost(id: number): Observable<Post> {
     const urlApi = `${this.URL}/Posts/ExcluirPost/${id}`;
     return this.http.delete<Post>(urlApi);
  }

}
