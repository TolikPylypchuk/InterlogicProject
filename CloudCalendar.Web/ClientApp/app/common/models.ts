﻿export interface Entity {
	id?: number;
}

export interface NameableEntity extends Entity {
	name?: string;
}

export interface EntityWithUser extends Entity {
	userId?: string;
	userFirstName?: string;
	userMiddleName?: string;
	userLastName?: string;
	userFullName?: string;
	userEmail?: string;
}

export interface GenericUser<T> {
	id?: T;
	email?: string;
	firstName?: string;
	middleName?: string;
	lastName?: string;
	fullName?: string;
	roles?: string[];
}

export interface Building extends Entity {
	name?: string;
	address?: string;
}

export interface Class extends Entity {
	subjectId?: number;
	subjectName?: string;
	dateTime?: string;
	type?: string;
	homeworkEnabled?: boolean;
}

export interface Classroom extends Entity {
	name?: string;
	buildingId?: number;
	buildingName?: string;
	buildingAddress?: string;
}

export interface Comment extends EntityWithUser {
	classId?: number;
	text?: string;
	dateTime?: string;
}

export interface Department extends NameableEntity {
	facultyId?: number;
}

export interface Faculty extends NameableEntity {
}

export interface Group extends NameableEntity {
	year?: number;
	curatorId?: number;
}

export interface Homework extends Entity {
	fileName?: string;
	studentId?: number;
	classId?: number;
	dateTime?: string;
	accepted?: boolean;
}

export interface Lecturer extends GenericUser<number> {
	departmentId?: number;
	departmentName?: string;
	userId?: string;
	isDean?: boolean;
	isHead?: boolean;
	isAdmin?: boolean;
}

export interface Material extends Entity {
	classId?: number;
	fileName?: string;
}

export interface Notification extends EntityWithUser {
	text?: string;
	dateTime?: string;
	isSeen?: boolean;
	classId?: number;
}

export interface Student extends GenericUser<number> {
	groupId?: number;
	groupName?: string;
	isGroupLeader?: boolean;
	transcriptNumber?: string;
	userId?: string;
}

export interface Subject extends NameableEntity { }

export interface User extends GenericUser<string> { }

export interface Semester {
	start?: string;
	end?: string;
}

export interface ScheduleOptions {
	classStarts?: string[];
	classEnds?: string[];
	classDuration?: string;
	firstSemester?: Semester;
	secondSemester?: Semester;
}
