import { ComponentFixture, TestBed } from '@angular/core/testing';

import { EditUploadedDocument } from './edit-uploaded-document';

describe('EditUploadedDocument', () => {
  let component: EditUploadedDocument;
  let fixture: ComponentFixture<EditUploadedDocument>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [EditUploadedDocument],
    }).compileComponents();

    fixture = TestBed.createComponent(EditUploadedDocument);
    component = fixture.componentInstance;
    await fixture.whenStable();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
