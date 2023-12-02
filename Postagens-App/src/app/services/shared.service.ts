import { Injectable } from '@angular/core';
import { Subject } from 'rxjs';

@Injectable({
  providedIn: 'root'
})
export class SharedService {

  constructor() { }

  private exclusaoConcluidaSubject = new Subject<void>();
  private uploadConcluidoSubject = new Subject<void>();
  exclusaoConcluida$ = this.exclusaoConcluidaSubject.asObservable();
  uploadConcluido$ = this.uploadConcluidoSubject.asObservable();

  notificarExclusaoConcluida() {
    this.exclusaoConcluidaSubject.next();
  }

  notificarUploadConcluido() {
    this.uploadConcluidoSubject.next();
  }
}
