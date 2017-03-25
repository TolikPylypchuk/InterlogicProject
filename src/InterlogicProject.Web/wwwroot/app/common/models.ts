﻿export interface Entity {
	id?: number;
}

export interface User extends Entity {
	email?: string;
	firstName?: string;
	middleName?: string;
	lastName?: string;
	fullName?: string;
}

export interface EntityWithUser extends Entity {
	userId?: number;
	userFirstName?: string;
	userMiddleName?: string;
	userLastName?: string;
	userFullName?: string;
	userEmail?: string;
}

export interface Building extends Entity {
	name: string;
	address?: string;
}

export interface Class extends Entity {
	subjectId?: number;
	subjectName?: string;
	dateTime?: string;
	type?: string;
	homeworkEnabled?: boolean;
}

export interface ClassPlace extends Entity {
	classId?: number;
	classroomId?: number;
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

export interface Department extends Entity {
	name?: string;
	facultyId?: number;
}

export interface Faculty extends Entity {
	name?: string;
	buildingId?: number;
	buildingName?: string;
	buildingAddress?: string;
}

export interface Group extends Entity {
	name?: string;
	year?: number;
	curatorId?: number;
}

export interface GroupClass extends Entity {
	groupId?: number;
	classId?: number;
}

export interface Homework extends Entity {
	studentId?: number;
	classId?: number;
	dateTime?: string;
	accepted?: boolean;
}

export interface Lecturer extends EntityWithUser {
	departmentId?: number;
	departmentName?: string;
	isDean?: boolean;
	isHead?: boolean;
	isAdmin?: boolean;
}

export interface LecturerClass extends Entity {
	lecturerId?: number;
	classId?: number;
}

export interface Material extends Entity {
	classId?: number;
	fileName?: string;
}

export interface Student extends EntityWithUser {
	groupId?: number;
	groupName?: string;
	isGroupLeader?: boolean;
	transcriptNumber?: string;
}

export interface Subject extends Entity {
	name?: string;
}