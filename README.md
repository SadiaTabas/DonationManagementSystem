# Donation Management System

A web-based Donation Management System developed to facilitate transparent and efficient donation collection, campaign management, volunteer coordination, and donor engagement. The system provides separate functionalities for Administrators, Volunteers, and Donors, enabling effective management of charitable activities.

---

## Features

### Common Features

* User Registration and Authentication
* Secure Login and Logout
* Change Password Functionality
* Profile Management
* Role-Based Access Control (Admin, Volunteer, Donor)

---

## Donor Module

### Dashboard

* View personal donation statistics
* Track recent donation activities

### Donation Management

* Make donations to different campaigns
* Select donation sectors (Zakat, Education, Health, etc.)
* Choose payment methods
* View donation status

### Donation History

* View previous donation records
* Track donation dates and amounts

### Feedback System

* Submit feedback and suggestions
* View submitted comments

---

## Volunteer Module

### Campaign Assignment

* View assigned campaigns
* Participate in campaign activities

### Donation Support

* Assist donors during donation campaigns
* Coordinate with administrators

### Campaign Monitoring

* Track campaign progress
* Manage campaign-related activities

---

## Admin Module

### Dashboard

* Monitor total donations
* View active campaigns
* Manage users and volunteers
* Track donation statistics

### User Management

* Add, update, and manage users
* Assign roles (Admin, Volunteer, Donor)
* Activate or deactivate user accounts

### Campaign Management

* Create new campaigns
* Update campaign information
* Manage campaign status
* Track campaign duration and venue

### Sector Management

* Create and manage donation sectors
* Update sector information
* Monitor sector activity

### Donation Monitoring

* Approve or reject donations
* Track payment methods
* Generate donation reports

### Feedback Management

* Review donor feedback
* Monitor user satisfaction

---

## Database Structure

### Tables

#### USSER

Stores user information including:

* UserId (Primary Key)
* Name
* FullName
* Email
* PhoneNo
* Gender
* DateOfBirth
* Address
* Password
* SecurityAns
* Role
* UserStatus

#### CAMPAIGNS

Stores campaign details:

* CampaignId (Primary Key)
* Name
* Description
* StartDate
* EndDate
* Venue
* Status

#### SECTORS

Stores donation sector information:

* SectorId (Primary Key)
* Title
* Description
* SectorStatus

#### DONATIONS

Stores donation records:

* DonationId (Primary Key)
* Amount
* DonationStatus
* DonationDate
* PayMethod
* CampaignId
* SectorId
* UserId

#### FEEDBACK

Stores donor feedback:

* FeedbackId (Primary Key)
* SubmissionDate
* UserId
* Comment

#### VOLCAMAS

Stores volunteer campaign assignments:

* AssignmentId (Primary Key)
* UserId
* CampaignId
* AssignmentDate

---

## Technology Stack

### Frontend

* HTML5
* CSS3
* Bootstrap
* JavaScript

### Backend

* C#
* ASP.NET Framework / Windows Forms

### Database

* Microsoft SQL Server

### Development Tools

* Visual Studio
* SQL Server Management Studio (SSMS)

---

## Sample Donation Sectors

* Zakat
* Education
* Health Care

## Sample Campaigns

* Ramadan Zakat Drive
* General Fundraiser
* Health Camp
* Summer Food Drive

---

## Installation Guide

### Clone Repository

```bash
git clone https://github.com/your-username/Donation-Management-System.git
```

### Database Setup

1. Open SQL Server Management Studio (SSMS).
2. Create a new database named:

```sql
Donation_Management_System
```

3. Execute the provided SQL script (`Donation_Management_System.sql`).

### Configure Database Connection

Update your connection string in the application configuration file:

```csharp
Server=YOUR_SERVER_NAME;
Database=Donation_Management_System;
Trusted_Connection=True;
TrustServerCertificate=True;
```

### Run the Application

1. Open the solution in Visual Studio.
2. Restore NuGet packages.
3. Build the solution.
4. Run the project.

---

## Future Enhancements

* Online Payment Gateway Integration
* Donation Analytics Dashboard
* Email Notifications
* Campaign Progress Tracking
* Mobile Application Support
* PDF Report Generation

---

## Project Objective

The primary objective of this project is to provide a transparent, secure, and user-friendly platform for managing charitable donations, campaigns, volunteers, and donor interactions while ensuring accountability and efficient fund distribution.

---

## License

This project is developed for educational and academic purposes.

