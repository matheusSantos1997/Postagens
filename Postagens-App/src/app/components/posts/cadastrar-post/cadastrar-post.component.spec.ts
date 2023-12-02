import { ComponentFixture, TestBed } from '@angular/core/testing';

import { CadastrarPostComponent } from './cadastrar-post.component';

describe('CadastrarPostComponent', () => {
  let component: CadastrarPostComponent;
  let fixture: ComponentFixture<CadastrarPostComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ CadastrarPostComponent ]
    })
    .compileComponents();
  });

  beforeEach(() => {
    fixture = TestBed.createComponent(CadastrarPostComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
