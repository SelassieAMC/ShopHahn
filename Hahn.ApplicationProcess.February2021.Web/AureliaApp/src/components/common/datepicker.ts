import { customElement, bindable, inject } from "aurelia-framework";
import * as moment from 'moment';
import * as $ from 'jquery'
@customElement("calendar")
@inject(Element)
export class Datepicker {
    private today: moment.Moment;

    @bindable()
    currentDate: Date;

    element: Element
    constructor(element: Element) {
        this.today = moment();
        this.currentDate = this.today.toDate();
        this.element = element;
    }

    private getDisplayDates(): Array<Array<moment.Moment>> {
        let dates = new Array<Array<any>>();
        let beginning = moment(this.currentDate).startOf('month').startOf('week');
        let currentMonth = this.currentDate.getMonth();
        for (let r = 0; r < 6; r++) {
            let week = new Array<any>();
            for (let i = 0; i < 7; i++) {
                let date = {
                    dayOfWeek: beginning.format('dddd'),
                    date: beginning.date(),
                    darken: beginning.month() != currentMonth,
                    events: []
                }
//                if ((i * r) % 4 == 0) {
//                    date.events.push({ name: i.toString() //+ '-' + r.toString() + 'event', amount: i * r + 10 })
//                }
                week.push(date);
                beginning.add(1, 'days');
            }
            dates.push(week);
        }
        return dates
    }

    attached() {
        let that = this;
        var test = $(".calendarDay", $(this.element)).on('click', function (event) {
            let clickEvent = new CustomEvent('day-click', {
                detail: { value: event.target },
                bubbles: true
            })
            that.element.dispatchEvent(clickEvent);
        });
    }
}
