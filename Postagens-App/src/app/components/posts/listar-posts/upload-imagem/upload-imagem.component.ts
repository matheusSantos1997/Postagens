import { Component, Inject, OnInit } from '@angular/core';
import { FormBuilder, FormControl, FormGroup, Validators } from '@angular/forms';
import { MAT_DIALOG_DATA } from '@angular/material/dialog';
import { MatSnackBar } from '@angular/material/snack-bar';
import { HTMLInputEvent } from 'src/app/components/customs/html-input-event';
import { IconSnackBarComponent } from 'src/app/components/customs/icon-snack-bar.component';
import { Imagem } from 'src/app/models/imagem';
import { Post } from 'src/app/models/post';
import { ImagemService } from 'src/app/services/imagem.service';
import { PostService } from 'src/app/services/post.service';
import { SharedService } from 'src/app/services/shared.service';

@Component({
  selector: 'app-upload-imagem',
  templateUrl: './upload-imagem.component.html',
  styleUrls: ['./upload-imagem.component.css']
})
export class UploadImagemComponent implements OnInit {

   postId: number;
   file: File;
   post: Post;
   formulario: FormGroup;

   constructor(@Inject (MAT_DIALOG_DATA) public dados: any,
                                         private postService: PostService,
                                         private imagemService: ImagemService,
                                         private sharedService: SharedService,
                                         private fb: FormBuilder,
                                         private snackBar: MatSnackBar) { }

   ngOnInit(): void {
    console.log(this.dados);
     this.validarFormulario();
   }

   validarFormulario() {
       this.formulario = this.fb.group({
         nome: new FormControl(null, [Validators.required]),
         postId: new FormControl(null)
       })
   }

   onFileChange(event: HTMLInputEvent) {
      this.file = event.target.files[0] as File;

      if (this.file) {
        const reader = new FileReader();

        if (this.file.type !== 'image/jpeg' && this.file.type !== 'image/png') {
            this.snackBar.open("Formato inválido", null, {
                duration: 2000,
                panelClass: ['snackbar-error'],
                horizontalPosition: 'right',
                verticalPosition: 'top'
            });
        }  else {
            reader.onload = (e: any) => {
                document.getElementById('foto').removeAttribute('hidden'); // remove o atributo hidden
                document.getElementById('foto').setAttribute('src', e.target.result); // seta a imagem
                console.log(e);
            }
       }

       reader.readAsDataURL(this.file);
    }  else {
        // Lidar com o caso em que nenhum arquivo foi selecionado, por exemplo, mostrar uma mensagem de erro
        console.log('Nenhum arquivo selecionado');
        return;
   }

  }

  salvarImagem() {
     this.postService.getPostById(this.dados.post.id).subscribe((response) => {
       const formData: FormData = new FormData();

       if(this.file !== null) {
          formData.append('file', this.file, this.file.name); // adiciona ao formData
       }

       this.imagemService.uploadImagem(response.id, formData).subscribe((response) => {
        const dadosRegistro: Imagem = new Imagem();
        dadosRegistro.URLImagem = response.URLImagem;
        dadosRegistro.postId = response.postId;

          console.log('imagem inserida com sucesso!');
          this.snackBar.openFromComponent(IconSnackBarComponent, {
            data: {
              icon: 'done',
              message: ' imagem inserida com sucesso!'
            },
            duration: 2000,
            panelClass: ['snackbar-success'],
            horizontalPosition: 'right',
            verticalPosition: 'top'
         });

         this.sharedService.notificarUploadConcluido();

       }, (error) => {
        this.snackBar.openFromComponent(IconSnackBarComponent, {
          data: {
            icon: 'gpp_bad',
            message: ` Erro ao inserir ${error}`
          },
          duration: 2000,
          panelClass: ['snackbar-error'],
          horizontalPosition: 'right',
          verticalPosition: 'top'
        });
        console.log('erro: ', error);
       })
     })
  }

}
