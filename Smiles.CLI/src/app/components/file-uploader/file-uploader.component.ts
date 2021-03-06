import { Component, OnDestroy, OnInit } from '@angular/core';
import { UploadService } from "../../services/upload.service";
import { catchError, map, takeUntil } from "rxjs/operators";
import { of, Subject } from "rxjs";

@Component({
  selector: 'app-file-uploader',
  templateUrl: 'file-uploader.template.html',
  styleUrls: ['file-uploader.component.css']
})
export class FileUploaderComponent implements OnInit, OnDestroy {
  public selectedFile: File;
  public uploadIsDisabled = true;
  public buttonText = "Upload";

  private destroy$: Subject<boolean> = new Subject<boolean>();

  constructor(private uploadService: UploadService) { }

  onFileSelected(event: any): void {
    this.selectedFile = event.target.files[0];

    if (this.selectedFile) {
      const fileSizeInMB = (this.selectedFile.size / (1024*1024));
      if (fileSizeInMB > 2) {
        alert('File is bigger than 2MB, choose another one!');
        this.uploadIsDisabled = true;
        this.selectedFile = undefined;
      }
      else {
        this.uploadIsDisabled = false;
      }
    }
  }

  onUploadClick() {
    if (this.selectedFile) {
      const formData = new FormData();
      formData.append("smiles", this.selectedFile);
      const upload$ = this.uploadService.uploadFile(formData);
      upload$.pipe(
          takeUntil(this.destroy$),
          map(count => {
            if (count > 0) {
              alert(`${count} SMILES entity is uploaded to the DB`);
              this.buttonText = "Upload";
            } else {
              alert(`Could not upload to the db`);
              this.buttonText = "Try again"
            }
          }),
          catchError(err => {
            alert("Error occured during contacting server");
            this.buttonText = "Try again"
            return of(err);
          })
      ).subscribe();
    }
  }

  ngOnInit(): void {
  }

  ngOnDestroy(): void {
    this.destroy$.next(true);
    this.destroy$.unsubscribe();
  }

}
