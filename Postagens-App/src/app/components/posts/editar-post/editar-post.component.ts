import { Component, OnInit } from '@angular/core';
import { FormBuilder, FormControl, FormGroup, Validators } from '@angular/forms';
import { MatSnackBar } from '@angular/material/snack-bar';
import { ActivatedRoute, ParamMap, Router } from '@angular/router';
import { forkJoin } from 'rxjs';
import { Imagem } from 'src/app/models/imagem';
import { Post } from 'src/app/models/post';
import { PostService } from 'src/app/services/post.service';
import { HTMLInputEvent } from '../../customs/html-input-event';
import { ImagemService } from 'src/app/services/imagem.service';
import { IconSnackBarComponent } from '../../customs/icon-snack-bar.component';
import { PostUpdateDTO } from 'src/app/models/dtos/postUpdateDto';

@Component({
  selector: 'app-editar-post',
  templateUrl: './editar-post.component.html',
  styleUrls: ['./editar-post.component.css']
})
export class EditarPostComponent implements OnInit {

  post: Post;
  imagem: Imagem;
  imagemPath: string;
  file: File;

  formulario: FormGroup;

  constructor(private postService: PostService,
              private imagemService: ImagemService,
              private fb: FormBuilder,
              private router: Router,
              private route: ActivatedRoute,
              private snackBar: MatSnackBar) { }

  ngOnInit(): void {
     this.validarFormulario();
  }

  validarFormulario() {
    this.route.paramMap.subscribe((params: ParamMap) => {
      const id =  +params.get('id');

      let post = this.postService.getPostById(id);

      forkJoin([post]).subscribe(([resultado]) => {
         this.post = resultado;
         this.imagem = resultado.imagem;
         if (this.imagem && this.imagem.nome) {
          this.imagemPath = `http://localhost:5245/Images/${this.imagem.nome}`;
        } else {
          this.imagemPath = ''; // ou outra ação apropriada, dependendo do seu caso
        }

        if(resultado.imagem) {
          const formGroupConfig = {
            id: new FormControl(resultado.id),
            titulo: new FormControl(resultado.titulo, [Validators.required]),
            conteudo: new FormControl(resultado.conteudo, [Validators.required]),
            imagem: this.fb.group({
                id: new FormControl(resultado.imagem ? resultado.imagem.id : null),
                nome: new FormControl(resultado.imagem ? resultado.imagem.nome : null),
                urlImagem: new FormControl(resultado.imagem ? resultado.imagem.URLImagem : null),
                postId: new FormControl(resultado.imagem ? resultado.imagem.postId : null)
              })
          };

          this.formulario = this.fb.group(formGroupConfig);
        } else {
          const formGroupConfig = {
            id: new FormControl(resultado.id),
            titulo: new FormControl(resultado.titulo, [Validators.required]),
            conteudo: new FormControl(resultado.conteudo, [Validators.required])
          };

          this.formulario = this.fb.group(formGroupConfig);
        }

      });
   });
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
        } else {
            reader.onload = (e: any) => {
                document.getElementById('foto').removeAttribute('hidden'); // remove o atributo hidden
                document.getElementById('foto').setAttribute('src', e.target.result); // seta a imagem
                console.log(e);
            }
        }

        reader.readAsDataURL(this.file);
    } else {
        // Lidar com o caso em que nenhum arquivo foi selecionado, por exemplo, mostrar uma mensagem de erro
        console.error('Nenhum arquivo selecionado');
        return;
    }

  }

  voltar() {
    this.router.navigate(['/']);
  }

  salvarPost() {

      const form = this.formulario.value;

      this.postService.updatePost(this.post.id, form).subscribe((post) => {
        if(this.file) {
          const formData: FormData = new FormData();

          formData.append('file', this.file, this.file.name );

         this.imagemService.putImagem(this.imagem.id, formData).subscribe((imagem) => {
           const dadosRegistro: Imagem = new Imagem();
           dadosRegistro.URLImagem = imagem.URLImagem;
           dadosRegistro.postId = imagem.postId;
           this.router.navigate(['/']);
           this.snackBar.openFromComponent(IconSnackBarComponent, {
              data: {
                icon: 'done',
                message: ' post e/ou imagem atualizados com sucesso!'
              },
              duration: 2000,
              panelClass: ['snackbar-success'],
              horizontalPosition: 'right',
              verticalPosition: 'top'
           });
           console.log('post e/ou imagem atualizados com sucesso!');
          }, (error) => {
            this.snackBar.openFromComponent(IconSnackBarComponent, {
              data: {
                icon: 'gpp_bad',
                message: ` Erro ao atualizar ${error}`
              },
              duration: 2000,
              panelClass: ['snackbar-error'],
              horizontalPosition: 'right',
              verticalPosition: 'top'
            });
            console.log('erro: ', error);
          })

        } else {
          this.router.navigate(['/']);
          this.snackBar.openFromComponent(IconSnackBarComponent, {
            data: {
              icon: 'done',
              message: ' post e/ou imagem atualizados com sucesso!'
            },
            duration: 2000,
            panelClass: ['snackbar-success'],
            horizontalPosition: 'right',
            verticalPosition: 'top'
         });
          console.log('post e/ou imagem atualizados com sucesso!');
        }

      });


  }

}
