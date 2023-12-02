import { Component, OnInit } from '@angular/core';
import { FormBuilder, FormControl, FormGroup, Validators } from '@angular/forms';
import { MatSnackBar } from '@angular/material/snack-bar';
import { Router } from '@angular/router';
import { PostService } from 'src/app/services/post.service';
import { IconSnackBarComponent } from '../../customs/icon-snack-bar.component';

@Component({
  selector: 'app-cadastrar-post',
  templateUrl: './cadastrar-post.component.html',
  styleUrls: ['./cadastrar-post.component.css']
})
export class CadastrarPostComponent implements OnInit {

  formulario: FormGroup;

  constructor(private fb: FormBuilder,
              private router: Router,
              private postService: PostService,
              private snackBar: MatSnackBar) { }

  ngOnInit(): void {
    this.validarFormulario();
  }

  validarFormulario() {
      this.formulario = this.fb.group({
        titulo: new FormControl(null, [Validators.required]),
        conteudo: new FormControl(null, [Validators.required])
      });
  }

  salvarPost() {
     const form = this.formulario.value;

     this.postService.createNewPost(form).subscribe((resp) => {
      this.router.navigate(['/']);
      this.snackBar.openFromComponent(IconSnackBarComponent, {
      data: {
        icon: 'done',
        message: ' Post Cadastrado com sucesso!'
      },
       duration: 2000,
       panelClass: ['snackbar-success'],
       horizontalPosition: 'right',
       verticalPosition: 'top'
     });
     }, (error) => {
      console.error(error);
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
     })
  }

  voltarListagem() {
     this.router.navigateByUrl('/');
  }



}
