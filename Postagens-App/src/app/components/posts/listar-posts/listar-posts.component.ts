import { AfterViewInit, Component, ElementRef, OnInit, ViewChild } from '@angular/core';
import { MatDialog } from '@angular/material/dialog';
import { MatTableDataSource } from '@angular/material/table';
import { Imagem } from 'src/app/models/imagem';
import { Post } from 'src/app/models/post';
import { PostService } from 'src/app/services/post.service';
import { UploadImagemComponent } from './upload-imagem/upload-imagem.component';
import { Router } from '@angular/router';
import { ConfirmarExclusaoPostImagemComponent } from './confirmar-exclusao-post-imagem/confirmar-exclusao-post-imagem.component';
import { SharedService } from 'src/app/services/shared.service';
import { Pagination } from 'src/app/models/pagination/pagination';
import { MatPaginator, PageEvent } from '@angular/material/paginator';

@Component({
  selector: 'app-listar-posts',
  templateUrl: './listar-posts.component.html',
  styleUrls: ['./listar-posts.component.css']
})
export class ListarPostsComponent implements OnInit, AfterViewInit  {

  imagemLargura: number = 150;
  imagemMargem: number = 10;

  pagination: Pagination;
  pageEvent: PageEvent;

  @ViewChild(MatPaginator, { static: true })
  paginator: MatPaginator;

  @ViewChild('paginator', { read: ElementRef })
  paginatorRef: ElementRef;

  mostrarPaginator: boolean = true;

  posts = new MatTableDataSource<Post>();
  displayedColumns: string[];
  caminhoImagem: string = 'http://localhost:5245/Images/';

  constructor(private postService: PostService,
              private dialog: MatDialog,
              private router: Router,
              private sharedService: SharedService) { }
  ngAfterViewInit(): void {
    // Esconder o paginator inicialmente
    this.paginatorRef.nativeElement.style.display = 'none';
  }

  ngOnInit(): void {
     this.pagination = { currentPage: 1, itemsPerPage: 3, totalItems: 1} as Pagination;
     this.listarTodosOsPosts();
     this.sharedService.exclusaoConcluida$.subscribe(() => {
      // Atualize a lista de posts quando a exclusão for concluída
      this.pagination = { currentPage: 1, itemsPerPage: 3, totalItems: 1} as Pagination;

      this.listarTodosOsPosts();
    });

    this.sharedService.uploadConcluido$.subscribe(() => {
      this.listarTodosOsPosts();
    })
  }

  exibirColunas(): string[]{
    return ['titulo', 'conteudo', 'imagem', 'data', 'actions']
  }

  abrirModalUploadImagem(imagem: Imagem, post: Post) {
      this.dialog.open(UploadImagemComponent, {
         data: {
          imagem,
          post
         },
         width: '90%',
         height: '70%'
      }).afterClosed().subscribe((response: boolean) => {
         if(response === true) {
            // Atualiza o post existente com o novo post ou adiciona à lista se não existir
            const index = this.posts.data.findIndex(p => p.id === post.id);
            if (index !== -1) {
               this.posts.data[index] = post;
            } else {
               this.posts.data.push(post);
            }

            // Notificar outros componentes sobre a conclusão do upload
            this.sharedService.notificarUploadConcluido();
         }
      })
  }

  abrirModalExcluirPostImagem(idPost: number, idImagem: number | null) {
     this.dialog.open(ConfirmarExclusaoPostImagemComponent, {
      data: {
        idPost: idPost,
        idImagem: idImagem
      }
     }).afterClosed().subscribe((response: boolean) => {
        if(response === true) {
          // Navegar para a mesma rota
          // this.router.navigateByUrl('/', { skipLocationChange: true }).then(() => {
          //     this.router.navigate([this.router.url]);
          // });
          // Notificar outros componentes sobre a exclusão
          this.sharedService.notificarExclusaoConcluida();
        }
     })
  }

  listarTodosOsPosts() {
     this.postService.getAllPosts(this.pagination.currentPage, this.pagination.itemsPerPage).subscribe((response) => {
        this.posts.data = response.result;
        this.pagination = response.pagination;

        this.getPaginationTranslateActions();

        // Atualiza a variável mostrarPaginator
      this.mostrarPaginator = this.posts.data.length > 0;

      // Mostra ou esconde o paginator com base na condição
      this.paginatorRef.nativeElement.style.display = this.mostrarPaginator ? 'block' : 'none';

        console.log(this.posts.data);
     }, (error) => {
       console.log(error);
     });

     this.displayedColumns = this.exibirColunas();
  }

  pageChanged(event: PageEvent) {

    let page = event.pageIndex;
    let size = event.pageSize;

    page = page + 1;

    this.pagination.currentPage = page;

    this.pagination.itemsPerPage = size;
    this.pagination.totalItems = event.length;
    this.listarTodosOsPosts();
  }

  private getPaginationTranslateActions() {
    // renomeia os nomes dos botoes da paginação
    this.paginator._intl.itemsPerPageLabel = 'Itens por página';
    this.paginator._intl.firstPageLabel = 'Primeira página';
    this.paginator._intl.lastPageLabel = 'Última página';
    this.paginator._intl.nextPageLabel = 'Próxima página';
    this.paginator._intl.previousPageLabel = 'Página anterior';
    this.paginator._intl.getRangeLabel = (page: number, pageSize: number, length: number) => {
        return(
          page * pageSize + 1 + ' - ' + (page * pageSize + pageSize) + ' de ' + length
        );
    };
  }

}
