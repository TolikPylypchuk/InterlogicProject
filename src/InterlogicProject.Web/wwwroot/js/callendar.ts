﻿/// <reference path="interfaces.ts" />

// import * as moment from "moment";
declare var moment: any;

$(document).ready(() => {
	$("#calendar").fullCalendar({
		allDaySlot: false,
		defaultView: "agendaWeek",
		eventClick: eventClicked,
		eventColor: "#0275D8",
		events: getEvents,
		weekends: false,
		weekNumbers: true
	});

	$("#calendar").fullCalendar("option", "height", "auto");
	$("#calendar").fullCalendar("option", "minTime", "08:00:00");
	$("#calendar").fullCalendar("option", "maxTime", "21:00:00");
});

function getEvents(
	start: any,
	end: any,
	timezone: string | boolean,
	callback: (data: FC.EventObject[]) => void): void {
	"use strict";

	$.get({
		url: "http://localhost:8000/api/classes/range/" +
			 `${start.format("YYYY-MM-DD")}/${end.format("YYYY-MM-DD")}`,
		success: (data: Class[]) => {
			callback(data.map(classToEvent));
		}
	});
}

function eventClicked(event: FC.EventObject): void {
	"use strict";

	$.get({
		url: `http://localhost:8000/api/classes/id/${event.id}`,
		async: true,
		success: (data: Class) => {
			$("#classTitle").text(data.subjectName);
			$("#classType").text(data.type);
			$("#classTime").text(moment.utc(data.dateTime)
				.format("DD.MM.YYYY, dddd, HH:mm"));

			$("#classInfoModal").modal("show");
		}});
}

function classToEvent(classInfo: Class): FC.EventObject {
	"use strict";

	return {
		id: classInfo.id,
		title: `${classInfo.subjectName}: ${classInfo.type}`,
		start: moment.utc(classInfo.dateTime).format(),
		end: moment.utc(classInfo.dateTime)
				   .add(1, "hours")
				   .add(20, "minutes")
				   .format()
	};
}
