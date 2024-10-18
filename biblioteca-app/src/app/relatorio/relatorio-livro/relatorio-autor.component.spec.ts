import { ComponentFixture, TestBed } from '@angular/core/testing';

import { RelatorioAutorComponent } from './relatorio-autor.component';

describe('RelatorioAutorComponent', () => {
  let component: RelatorioAutorComponent;
  let fixture: ComponentFixture<RelatorioAutorComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [RelatorioAutorComponent]
    })
    .compileComponents();

    fixture = TestBed.createComponent(RelatorioAutorComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
