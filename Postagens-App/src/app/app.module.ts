import { BrowserModule } from '@angular/platform-browser';
import { NgModule } from '@angular/core';
import { HttpClientModule } from '@angular/common/http';

import { AppRoutingModule } from './app-routing.module';
import { AppComponent } from './app.component';
import { BrowserAnimationsModule } from '@angular/platform-browser/animations';
import { MaterialModule } from './material.module';
import { HeaderComponent } from './components/header/header.component';
import { ListarPostsComponent } from './components/posts/listar-posts/listar-posts.component';
import { CadastrarPostComponent } from './components/posts/cadastrar-post/cadastrar-post.component';
import { EditarPostComponent } from './components/posts/editar-post/editar-post.component';
import { FooterComponent } from './components/footer/footer.component';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { FlexLayoutModule } from '@angular/flex-layout';
import { IconSnackBarComponent } from './components/customs/icon-snack-bar.component';
import { UploadImagemComponent } from './components/posts/listar-posts/upload-imagem/upload-imagem.component';
import { ConfirmarExclusaoPostImagemComponent } from './components/posts/listar-posts/confirmar-exclusao-post-imagem/confirmar-exclusao-post-imagem.component';

@NgModule({
  declarations: [
    AppComponent,
    HeaderComponent,
    ListarPostsComponent,
    CadastrarPostComponent,
    EditarPostComponent,
    FooterComponent,
    IconSnackBarComponent,
    UploadImagemComponent,
    ConfirmarExclusaoPostImagemComponent
  ],
  imports: [
    BrowserModule,
    AppRoutingModule,
    BrowserAnimationsModule,
    HttpClientModule,
    FormsModule,
    ReactiveFormsModule,
    FlexLayoutModule,
    MaterialModule,
  ],
  providers: [],
  bootstrap: [AppComponent]
})
export class AppModule { }
