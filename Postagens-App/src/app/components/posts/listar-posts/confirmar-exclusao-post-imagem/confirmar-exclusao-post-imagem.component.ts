import { Component, Inject, OnInit } from '@angular/core';
import { MAT_DIALOG_DATA } from '@angular/material/dialog';
import { MatSnackBar } from '@angular/material/snack-bar';
import { take } from 'rxjs/operators';
import { IconSnackBarComponent } from 'src/app/components/customs/icon-snack-bar.component';
import { ImagemService } from 'src/app/services/imagem.service';
import { PostService } from 'src/app/services/post.service';
import { SharedService } from 'src/app/services/shared.service';

@Component({
  selector: 'app-confirmar-exclusao-post-imagem',
  templateUrl: './confirmar-exclusao-post-imagem.component.html',
  styleUrls: ['./confirmar-exclusao-post-imagem.component.css']
})
export class ConfirmarExclusaoPostImagemComponent implements OnInit {

  constructor(@Inject (MAT_DIALOG_DATA) public dados: any,
              private postService: PostService,
              private imagemService: ImagemService,
              private snackBar: MatSnackBar,
              private sharedService: SharedService) { }

  ngOnInit(): void {
    console.log(`id do post é ${this.dados.idPost} e da imagem é: ${this.dados.idImagem}`);
  }

  excluirPostImagem(idPost: number, idImagem: number | null) {

    if (idImagem !== null) {
      // Excluir a imagem
      this.imagemService.deleteImagem(idImagem).pipe(take(1)).subscribe(() => {
        // Após a exclusão da imagem, você pode excluir o post
        this.postService.deletePost(idPost).pipe(take(1)).subscribe(() => {
          console.log('Post excluído com sucesso!');
          this.snackBar.openFromComponent(IconSnackBarComponent, {
            data: {
              icon: 'done',
              message: ' post excluído com sucesso!'
            },
            duration: 2000,
            panelClass: ['snackbar-success'],
            horizontalPosition: 'right',
            verticalPosition: 'top'
         });

         // Notificar outros componentes sobre a exclusão
        this.sharedService.notificarExclusaoConcluida();

        });
      });
    } else {
      // Se não há imagem, exclua apenas o post
      this.postService.deletePost(idPost).pipe(take(1)).subscribe(() => {
        console.log('Post excluído com sucesso!');
        this.snackBar.openFromComponent(IconSnackBarComponent, {
          data: {
            icon: 'done',
            message: ' post excluído com sucesso!'
          },
          duration: 2000,
          panelClass: ['snackbar-success'],
          horizontalPosition: 'right',
          verticalPosition: 'top'
       });

       // Notificar outros componentes sobre a exclusão
       this.sharedService.notificarExclusaoConcluida();
      });
    }


  }

}
