import { timer } from "rxjs";
import { TimerEntry, TimerState } from "./../Shared/Models/TimerEntry";
import { TimerService } from "./../Services/TimerService.service";
import {
  AfterViewInit,
  ChangeDetectorRef,
  Component,
  OnInit,
  QueryList,
  ViewChild,
  ViewChildren,
} from "@angular/core";
import { MatChipInputEvent } from "@angular/material/chips";
import { COMMA, ENTER } from "@angular/cdk/keycodes";
import { FormControl, FormGroup } from "@angular/forms";
import { MatPaginator } from "@angular/material/paginator";
import { MatTableDataSource } from "@angular/material/table";

@Component({
  selector: "app-Timers-List",
  templateUrl: "./Timers-List.component.html",
  styleUrls: ["./Timers-List.component.css"],
})
export class TimersListComponent implements OnInit, AfterViewInit {
  @ViewChildren(MatPaginator) paginator = new QueryList<MatPaginator>();

  columndefs: string[] = [
    "description",
    "startDate",
    "dash",
    "endDate",
    "elapsed",
    "project",
    "labels",
    "options",
  ];

  visible = true;
  selectable = true;
  removable = true;
  addOnBlur = true;
  readonly separatorKeysCodes: number[] = [ENTER, COMMA];

  Data: MatTableDataSource<TimerEntry>;

  form = new FormGroup({
    labelsSelection: new FormControl([]),
  });

  get labels() {
    return this.form.get("labels");
  }

  add(event: MatChipInputEvent): void {
    const input = event.input;
    const value = event.value;

    if ((value || "").trim()) {
      this.labels.setValue([...this.labels.value, value.trim()]);
      this.labels.updateValueAndValidity();
    }

    // Reset the input value
    if (input) {
      input.value = "";
    }
  }

  remove(fruit: string): void {
    const index = this.labels.value.indexOf(fruit);

    if (index >= 0) {
      this.labels.value.splice(index, 1);
      this.labels.updateValueAndValidity();
    }
  }

  ini: TimerEntry[];

  refreshData() {
    var today = new Date();
    today.setHours(0, 0, 0, 0);

    this._timersService.GetTimerEntries().subscribe(
      (result) => {
        this.ini = result.filter(e => e.state === TimerState.Finished);

        this.buildDataSource();
      },
      (error) => console.log(error)
    );
  }

  public addTimer(timerEntry: TimerEntry) {
    this.ini.unshift(timerEntry);
    this.buildDataSource();
    this.Data._updateChangeSubscription();
  }

  ngOnInit() {
    this.Data = new MatTableDataSource<TimerEntry>();

    this.refreshData();
  }
  ngAfterViewInit() {
    this.Data.paginator = this.paginator.toArray()[0];
  }

  public deleteRow(id: number) {
    this._timersService.DeleteTimerEntry(id).subscribe(
      () => {
        const index = this.ini.findIndex((entry) => entry.id === id);
        this.ini.splice(index, 1);
        this.buildDataSource();
        this.Data._updateChangeSubscription();
      },
      (error) => console.log(error)
    );
  }

  constructor(private _timersService: TimerService) {}

  ///
  displayedColumns: string[];

  dataSource = [];

  groupingColumn = "startDate";

  reducedGroups = [];

  initialData: any[];

  /**
   * Discovers columns in the data
   */
  initData(data) {
    if (!data) return false;
    this.displayedColumns = Object.keys(data[0]);
    this.initialData = this.ini;
    return true;
  }

  /**
   * Rebuilds the datasource after any change to the criterions
   */
  buildDataSource() {
    this.Data.data = this.groupBy(
      this.groupingColumn,
      this.ini,
      this.reducedGroups
    );
  }

  public convertDate(date: Date): string {
    var today = new Date(date);
    var dd = String(today.getDate()).padStart(2, "0");
    var mm = String(today.getMonth() + 1).padStart(2, "0");
    var yyyy = today.getFullYear();

    return mm + "/" + dd + "/" + yyyy;
  }

  getGroupNameForDate(date: Date) {
    var entryDate = this.convertDate(date);
    var yesterday = new Date();
    yesterday.setDate(yesterday.getDate() - 1);

    if (entryDate === this.convertDate(new Date())) return "Today";
    if (entryDate === this.convertDate(yesterday)) return "Yesterday";
    else return this.convertDate(date);
  }
  /**
   * Groups the @param data by distinct values of a @param column
   * This adds group lines to the dataSource
   * @param reducedGroups is used localy to keep track of the colapsed groups
   */
  groupBy(column: string, data: any[], reducedGroups?: any[]) {
    if (!column) return data;
    let collapsedGroups = reducedGroups;
    if (!reducedGroups) collapsedGroups = [];
    const customReducer = (accumulator, currentValue) => {
      let currentGroup = this.getGroupNameForDate(
        new Date(currentValue[column])
      );
      if (!accumulator[currentGroup])
        accumulator[currentGroup] = [
          {
            groupName: `${this.getGroupNameForDate(currentValue[column])}`,
            value: new Date(currentValue[column]).getDate(),
            isGroup: true,
            reduced: collapsedGroups.some(
              (group) =>
                group.value == this.getGroupNameForDate(currentValue[column])
            ),
          },
        ];

      accumulator[currentGroup].push(currentValue);

      return accumulator;
    };
    let groups = data.reduce(customReducer, {});
    let groupArray = Object.keys(groups).map((key) => groups[key]);
    let flatList = groupArray.reduce((a, c) => {
      return a.concat(c);
    }, []);

    return flatList.filter((rawLine) => {
      return (
        rawLine.isGroup ||
        collapsedGroups.every((group) => rawLine[column] != group.value)
      );
    });
  }

  /**
   * Since groups are on the same level as the data,
   * this function is used by @input(matRowDefWhen)
   */
  isGroup(index, item): boolean {
    return item.isGroup;
  }

  /**
   * Used in the view to collapse a group
   * Effectively removing it from the displayed datasource
   */

  ///
}
