import { ProjectsService } from "./../Services/Projects.service";
import { TimerEntry, TimerState } from "./../Shared/Models/TimerEntry";
import { TimerService } from "./../Services/TimerService.service";
import { Label } from "./../Shared/Models/Label";
import { COMMA, ENTER } from "@angular/cdk/keycodes";
import {
  Component,
  OnInit,
  ViewEncapsulation,
  ViewChild,
  ElementRef,
  Output,
  AfterContentInit,
} from "@angular/core";
import { Observable, timer } from "rxjs";
import { MatAutocompleteSelectedEvent } from "@angular/material/autocomplete";
import { MatChipInputEvent } from "@angular/material/chips";
import { FormControl } from "@angular/forms";
import { Project } from "../Shared/Models/Project";
import { EventEmitter } from "@angular/core";
import { map, startWith } from "rxjs/operators";
import {Subject} from 'rxjs';
import {debounceTime, distinctUntilChanged} from 'rxjs/operators';


@Component({
  selector: "app-Stopwatch-Bar",
  templateUrl: "./Stopwatch-Bar.component.html",
  styleUrls: ["./Stopwatch-Bar.component.css"],
  encapsulation: ViewEncapsulation.None
})
export class StopwatchBarComponent implements OnInit, AfterContentInit {
  @Output() newEntryEvent: EventEmitter<TimerEntry> = new EventEmitter<TimerEntry>();
  @ViewChild("labelInput", { static: false }) labelInput: ElementRef<HTMLInputElement>;

  columndefs: string[] = ["description", "startTime", "endTime", "elapsed"];

  descriptionChanged: Subject<string> = new Subject<string>();

  constructor( private _timersService: TimerService, private projectsService: ProjectsService ) {}
  ngAfterContentInit(): void {

  }


  onDescriptionChanged(event: any) {
    if (this.descriptionChanged.observers.length === 0 && this.entry.state == TimerState.Running) {
        this.descriptionChanged.pipe(debounceTime(3000), distinctUntilChanged())
            .subscribe(term => {
                this.sendData();
            });
    }
    this.descriptionChanged.next(event);
}



  ngOnInit() {
      this.entry = new TimerEntry();
      let runningTimer: TimerEntry;
      this.isRunning = false;


      this._timersService.GetRunningTimer().subscribe(
        result =>
        {
          runningTimer = result;

          if(!runningTimer)  return

          this.entry = runningTimer;
          this.isRunning = true;
          this.entry.elapsed = Math.floor(this.entry.elapsed /1000);
        }
        );

      this.projectsService
      .getProjects()
      .subscribe((result) => (this.AllProjects = result));

      this._timersService.GetLabels().subscribe((result) => {
      this.allLabels = result;


      this.filteredLabels = this.labelCtrl.valueChanges.pipe(
        startWith(null),
        map((label: string | null) =>
          label
            ? this._filter(label)
            : this.allLabels
                .slice()
                .filter((label) => !this.entry.labels.includes(label))
        )
      );
    });



    timer(0, 1000).subscribe((ellapsedCycles) => {
      if (this.isRunning) {
        this.entry.elapsed++;

        this.TimeDisplay =
          ("0" + Math.floor(this.entry.elapsed / 3600)).slice(-2) +
          ":" +
          ("0" + Math.floor((this.entry.elapsed % 3600) / 60)).slice(-2) +
          ":" +
          ("0" + Math.floor((this.entry.elapsed % 3600) % 3600)).slice(-2);
      }
    });
  }

  entry: TimerEntry;


  set description(value: string) {
    this.entry.description = value;
    // this.sendData()
  }
  get description() {
    return this.entry.description;
  }



  set project(value: Project) {
    this.entry.project = value;
    this._timersService.SaveTemporaryData(this.entry);
  }
  get project() {
    return this.entry.project;
  }




  public isRunning: boolean = false;
  public seconds: number = 0;
  public minutes: number;
  public hours: number;
  public TimeDisplay: string = "00:00:00";



  selectable = true;
  removable = true;
  separatorKeysCodes: number[] = [ENTER, COMMA];
  labelCtrl = new FormControl();
  allLabels: Label[];
  filteredLabels: Observable<Label[]>;
  AllProjects: Project[];

  add(event: MatChipInputEvent): void {
    const value = (event.value || "").trim();

    if (!value) return;

    for (var i = 0; i < this.allLabels.length; i++) {
      if (
        this.allLabels[i].name.toLowerCase() === value.toLowerCase() &&
        !this.entry.labels.includes(this.allLabels[i])
      ) {
        this.entry.labels.push(this.allLabels[i]);
        break;
      }
    }
    event.input!.value = "";

    this.labelCtrl.setValue(null);
    if (this.entry.state == TimerState.Running) {
      this.sendData();
    }
  }

  remove(label: Label): void {
    const index = this.entry.labels.indexOf(label);

    if (index >= 0) {
      this.entry.labels.splice(index, 1);
    }
    if (this.entry.state == TimerState.Running) {
      this.sendData();
    }
  }

  selected(event: MatAutocompleteSelectedEvent): void {
    var lbl = this.allLabels.find(
      (label) =>
        label.name.toLowerCase() === event.option.viewValue.toLowerCase() &&
        !this.entry.labels.includes(label)

    );
    if (lbl) {
      this.entry.labels.push(lbl || lbl.name == "" ? lbl : null); //bruh
    }

    this.labelInput.nativeElement.value = "";
    this.labelCtrl.setValue(null);
    if (this.entry.state == TimerState.Running) {
      this.sendData();
    }
  }

  private _filter(value: string): Label[] {
    const filterValue = value.toLowerCase();

    var filtered = this.allLabels.filter(
      (label) =>
        label ===
        this.allLabels.find((lbl) =>
          lbl.name.toLowerCase().includes(filterValue)
        )
    );
    filtered = filtered.filter((label) => !this.entry.labels.includes(label));
    return filtered;
  }

  toggleTimer() {
    this.isRunning = !this.isRunning;

    if (this.isRunning) {

      this.entry.startDate = new Date();
      this.entry.state = TimerState.Running;
      this.sendData();
      return;
    }

    this.entry.endDate = new Date();
    this.entry.state = TimerState.Finished;

    this._timersService.AddOrUpdateEntry(this.entry).subscribe((result) => {
      if(result.state === TimerState.Finished) this.newEntryEvent.emit(result);
      this.clearData();
      this.TimeDisplay = '00:00:00'


    });
  }

  private clearData() {
    this.entry = new TimerEntry();
  }

  private sendData(){
    this._timersService.AddOrUpdateEntry(this.entry).subscribe(
      result=> this.entry.id = result.id
    );
  }

}


