import { MatDialogRef, MAT_DIALOG_DATA } from '@angular/material/dialog';
import { LabelsComponent } from './../Labels.component';
import { Component, Inject, OnInit } from '@angular/core';
import { Label } from '../../Shared/Models/Label';

@Component({
  selector: 'app-LabelsDialog',
  templateUrl: './LabelsDialog.component.html',
  styleUrls: ['./LabelsDialog.component.css']
})
export class LabelsDialogComponent implements OnInit {

  constructor(public dialogRef: MatDialogRef<LabelsComponent>,
    @Inject(MAT_DIALOG_DATA) public data: Label) { }

  ngOnInit() {
  }

}
