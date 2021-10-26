import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Inject, Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { Label } from '../Shared/Models/Label';
import { TimerEntry } from '../Shared/Models/TimerEntry';

@Injectable()
export class TimerService {

constructor(private http: HttpClient, @Inject('BASE\_URL') private baseUrl: string) {
 }

 GetTimerEntries(): Observable<TimerEntry[]> {
   return this.http.get<TimerEntry[]>(this.baseUrl + 'api/Entries');
 }

 GetLabels(): Observable<Label[]>{
   return this.http.get<Label[]>(this.baseUrl + 'api/Labels');
 }
 DeleteTimerEntry(id: number) {

  var req = this.http.delete(this.baseUrl + "api/DeleteTimerEntry/"+id);
  return req;

}

 AddOrUpdateEntry(entry: TimerEntry): Observable<TimerEntry> {

   const httpOptions = {
     headers: new HttpHeaders({ 'Content-Type': 'application/json' })
   }

    var req = this.http.post<TimerEntry>(this.baseUrl + "api/AddEntry", JSON.stringify(entry), httpOptions);
    console.log(req);
    return req;
 }

 GetRunningTimer(): Observable<TimerEntry>{
    var req = this.http.get<TimerEntry>(this.baseUrl +"api/GetRunningEntry");
    return req;
 }


 public SaveTemporaryData(timer: TimerEntry){
  localStorage.setItem("runningTimer", JSON.stringify(timer))
}
public RemoveTemporaryData() {
  localStorage.removeItem("runningTimer");
}
}
