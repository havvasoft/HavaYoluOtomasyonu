using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace HavaYoluOtomasyonu.Models;

public partial class HavayoluOtomasyonDbContext : DbContext
{
    public HavayoluOtomasyonDbContext()
    {
    }

    public HavayoluOtomasyonDbContext(DbContextOptions<HavayoluOtomasyonDbContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Aircraft> Aircrafts { get; set; }

    public virtual DbSet<Airport> Airports { get; set; }

    public virtual DbSet<Baggage> Baggages { get; set; }

    public virtual DbSet<Booking> Bookings { get; set; }

    public virtual DbSet<Fare> Fares { get; set; }

    public virtual DbSet<Flight> Flights { get; set; }

    public virtual DbSet<FlightCrew> FlightCrews { get; set; }

    public virtual DbSet<FlightStatusHistory> FlightStatusHistories { get; set; }

    public virtual DbSet<Gate> Gates { get; set; }

    public virtual DbSet<MaintenanceLog> MaintenanceLogs { get; set; }

    public virtual DbSet<Model> Models { get; set; }

    public virtual DbSet<Passenger> Passengers { get; set; }

    public virtual DbSet<Payment> Payments { get; set; }

    public virtual DbSet<Role> Roles { get; set; }

    public virtual DbSet<Route> Routes { get; set; }

    public virtual DbSet<Staff> Staff { get; set; }

    public virtual DbSet<TechnicalCertification> TechnicalCertifications { get; set; }

    public virtual DbSet<Ticket> Tickets { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see https://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseSqlServer("Server=.;Database=HavayoluOtomasyonDB;Trusted_Connection=True;TrustServerCertificate=True;");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Aircraft>(entity =>
        {
            entity.HasKey(e => e.AircraftId).HasName("PK__Aircraft__F75CBC0B610393DE");

            entity.HasIndex(e => e.TailNumber, "UQ__Aircraft__3F41D11B0B01BE94").IsUnique();

            entity.Property(e => e.AircraftId).HasColumnName("AircraftID");
            entity.Property(e => e.ModelId).HasColumnName("ModelID");
            entity.Property(e => e.Status)
                .HasMaxLength(20)
                .IsUnicode(false);
            entity.Property(e => e.TailNumber)
                .HasMaxLength(20)
                .IsUnicode(false);

            entity.HasOne(d => d.Model).WithMany(p => p.Aircraft)
                .HasForeignKey(d => d.ModelId)
                .HasConstraintName("FK__Aircrafts__Model__5812160E");
        });

        modelBuilder.Entity<Airport>(entity =>
        {
            entity.HasKey(e => e.AirportId).HasName("PK__Airports__E3DBE08AC7BC5174");

            entity.HasIndex(e => e.Iatacode, "UQ__Airports__EFD6F5BE71011663").IsUnique();

            entity.Property(e => e.AirportId).HasColumnName("AirportID");
            entity.Property(e => e.AirportName).HasMaxLength(150);
            entity.Property(e => e.City).HasMaxLength(100);
            entity.Property(e => e.Country).HasMaxLength(100);
            entity.Property(e => e.Iatacode)
                .HasMaxLength(3)
                .IsUnicode(false)
                .HasColumnName("IATACode");
            entity.Property(e => e.TimeZone)
                .HasMaxLength(50)
                .IsUnicode(false);
        });

        modelBuilder.Entity<Baggage>(entity =>
        {
            entity.HasKey(e => e.BaggageId).HasName("PK__Baggage__01AFFC05D6AA6703");

            entity.ToTable("Baggage");

            entity.HasIndex(e => e.BaggageTagNumber, "UQ__Baggage__8FE1297C1B8EF632").IsUnique();

            entity.Property(e => e.BaggageId).HasColumnName("BaggageID");
            entity.Property(e => e.BaggageTagNumber)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.Status)
                .HasMaxLength(20)
                .IsUnicode(false);
            entity.Property(e => e.TicketId).HasColumnName("TicketID");
            entity.Property(e => e.Weight).HasColumnType("decimal(5, 2)");

            entity.HasOne(d => d.Ticket).WithMany(p => p.Baggages)
                .HasForeignKey(d => d.TicketId)
                .HasConstraintName("FK__Baggage__TicketI__02FC7413");
        });

        modelBuilder.Entity<Booking>(entity =>
        {
            entity.HasKey(e => e.BookingId).HasName("PK__Bookings__73951ACDEF622085");

            entity.HasIndex(e => e.PnrCode, "UQ__Bookings__146F6C17A8E057E5").IsUnique();

            entity.Property(e => e.BookingId).HasColumnName("BookingID");
            entity.Property(e => e.BookingDate)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime");
            entity.Property(e => e.PaymentStatus)
                .HasMaxLength(20)
                .IsUnicode(false);
            entity.Property(e => e.PnrCode)
                .HasMaxLength(6)
                .IsUnicode(false)
                .HasColumnName("PNR_Code");
            entity.Property(e => e.TotalAmount).HasColumnType("decimal(18, 2)");

            entity.HasOne(d => d.CreatedByStaffNavigation).WithMany(p => p.Bookings)
                .HasForeignKey(d => d.CreatedByStaff)
                .HasConstraintName("FK__Bookings__Create__72C60C4A");
        });

        modelBuilder.Entity<Fare>(entity =>
        {
            entity.HasKey(e => e.FareId).HasName("PK__Fares__1261FA36DF664607");

            entity.Property(e => e.FareId).HasColumnName("FareID");
            entity.Property(e => e.CabinType)
                .HasMaxLength(20)
                .IsUnicode(false);
            entity.Property(e => e.FareBasicCode)
                .HasMaxLength(20)
                .IsUnicode(false);
        });

        modelBuilder.Entity<Flight>(entity =>
        {
            entity.HasKey(e => e.FlightId).HasName("PK__Flights__8A9E148EBFDF3769");

            entity.Property(e => e.FlightId).HasColumnName("FlightID");
            entity.Property(e => e.AircraftId).HasColumnName("AircraftID");
            entity.Property(e => e.ArrivalTime).HasColumnType("datetime");
            entity.Property(e => e.DepartureTime).HasColumnType("datetime");
            entity.Property(e => e.FlightStatus)
                .HasMaxLength(20)
                .IsUnicode(false);
            entity.Property(e => e.RoutesId).HasColumnName("RoutesID");

            entity.HasOne(d => d.Aircraft).WithMany(p => p.Flights)
                .HasForeignKey(d => d.AircraftId)
                .HasConstraintName("FK__Flights__Aircraf__66603565");

            entity.HasOne(d => d.Routes).WithMany(p => p.Flights)
                .HasForeignKey(d => d.RoutesId)
                .HasConstraintName("FK__Flights__RoutesI__656C112C");
        });

        modelBuilder.Entity<FlightCrew>(entity =>
        {
            entity.HasKey(e => e.FlightCrewId).HasName("PK__FlightCr__EBA8BEA8145591FE");

            entity.ToTable("FlightCrew");

            entity.Property(e => e.FlightCrewId).HasColumnName("FlightCrewID");
            entity.Property(e => e.AssigmentRole)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.FlightId).HasColumnName("FlightID");
            entity.Property(e => e.StaffId).HasColumnName("StaffID");

            entity.HasOne(d => d.Flight).WithMany(p => p.FlightCrews)
                .HasForeignKey(d => d.FlightId)
                .HasConstraintName("FK__FlightCre__Fligh__09A971A2");

            entity.HasOne(d => d.Staff).WithMany(p => p.FlightCrews)
                .HasForeignKey(d => d.StaffId)
                .HasConstraintName("FK__FlightCre__Staff__0A9D95DB");
        });

        modelBuilder.Entity<FlightStatusHistory>(entity =>
        {
            entity.HasKey(e => e.HistoryId).HasName("PK__FlightSt__4D7B4ADD81D266F1");

            entity.ToTable("FlightStatusHistory");

            entity.Property(e => e.HistoryId).HasColumnName("HistoryID");
            entity.Property(e => e.FlightId).HasColumnName("FlightID");
            entity.Property(e => e.Reason).HasMaxLength(255);
            entity.Property(e => e.Status)
                .HasMaxLength(20)
                .IsUnicode(false);
            entity.Property(e => e.UpdateTime)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime");

            entity.HasOne(d => d.Flight).WithMany(p => p.FlightStatusHistories)
                .HasForeignKey(d => d.FlightId)
                .HasConstraintName("FK__FlightSta__Fligh__05D8E0BE");
        });

        modelBuilder.Entity<Gate>(entity =>
        {
            entity.HasKey(e => e.GateId).HasName("PK__Gates__9582C63087ACDEA8");

            entity.Property(e => e.GateId).HasColumnName("GateID");
            entity.Property(e => e.AirportId).HasColumnName("AirportID");
            entity.Property(e => e.GateNumber)
                .HasMaxLength(10)
                .IsUnicode(false);
            entity.Property(e => e.Terminal)
                .HasMaxLength(50)
                .IsUnicode(false);

            entity.HasOne(d => d.Airport).WithMany(p => p.Gates)
                .HasForeignKey(d => d.AirportId)
                .HasConstraintName("FK__Gates__AirportID__628FA481");
        });

        modelBuilder.Entity<MaintenanceLog>(entity =>
        {
            entity.HasKey(e => e.LogId).HasName("PK__Maintena__5E5499A88FA1E557");

            entity.Property(e => e.LogId).HasColumnName("LogID");
            entity.Property(e => e.AircraftId).HasColumnName("AircraftID");
            entity.Property(e => e.LeadEngineerId).HasColumnName("LeadEngineerID");
            entity.Property(e => e.MaintenanceType)
                .HasMaxLength(50)
                .IsUnicode(false);

            entity.HasOne(d => d.Aircraft).WithMany(p => p.MaintenanceLogs)
                .HasForeignKey(d => d.AircraftId)
                .HasConstraintName("FK__Maintenan__Aircr__6D0D32F4");

            entity.HasOne(d => d.LeadEngineer).WithMany(p => p.MaintenanceLogs)
                .HasForeignKey(d => d.LeadEngineerId)
                .HasConstraintName("FK__Maintenan__LeadE__6E01572D");
        });

        modelBuilder.Entity<Model>(entity =>
        {
            entity.HasKey(e => e.ModelId).HasName("PK__Models__E8D7A1CC605FC5F9");

            entity.Property(e => e.ModelId).HasColumnName("ModelID");
            entity.Property(e => e.AvgSpeed).HasColumnType("decimal(6, 2)");
            entity.Property(e => e.CapacityBusiness).HasColumnName("Capacity_Business");
            entity.Property(e => e.CapacityEconomy).HasColumnName("Capacity_Economy");
            entity.Property(e => e.IcaoTypeCode)
                .HasMaxLength(10)
                .IsUnicode(false)
                .HasColumnName("ICAO_TypeCode");
            entity.Property(e => e.Manufacturer).HasMaxLength(100);
            entity.Property(e => e.ModelName).HasMaxLength(100);
        });

        modelBuilder.Entity<Passenger>(entity =>
        {
            entity.HasKey(e => e.PassengerId).HasName("PK__Passenge__88915F900C271A6F");

            entity.HasIndex(e => e.PassportNumber, "UQ__Passenge__45809E71D211EEFC").IsUnique();

            entity.HasIndex(e => e.Email, "UQ__Passenge__A9D10534F802EF02").IsUnique();

            entity.Property(e => e.PassengerId).HasColumnName("PassengerID");
            entity.Property(e => e.Email)
                .HasMaxLength(150)
                .IsUnicode(false);
            entity.Property(e => e.EmergencyContact)
                .HasMaxLength(150)
                .IsUnicode(false);
            entity.Property(e => e.FirstName).HasMaxLength(100);
            entity.Property(e => e.Gender)
                .HasMaxLength(10)
                .IsUnicode(false);
            entity.Property(e => e.LastName).HasMaxLength(100);
            entity.Property(e => e.LoyaltyProgramId)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("LoyaltyProgramID");
            entity.Property(e => e.Nationality).HasMaxLength(50);
            entity.Property(e => e.PassportNumber)
                .HasMaxLength(20)
                .IsUnicode(false);
            entity.Property(e => e.Phone)
                .HasMaxLength(20)
                .IsUnicode(false);
            entity.Property(e => e.SpecialNeeds).HasMaxLength(255);
        });

        modelBuilder.Entity<Payment>(entity =>
        {
            entity.HasKey(e => e.PaymentId).HasName("PK__Payments__9B556A58EAC52FBA");

            entity.HasIndex(e => e.TransactionId, "UQ__Payments__55433A4ABF3A3D91").IsUnique();

            entity.Property(e => e.PaymentId).HasColumnName("PaymentID");
            entity.Property(e => e.Amount).HasColumnType("decimal(18, 2)");
            entity.Property(e => e.BookingId).HasColumnName("BookingID");
            entity.Property(e => e.PaymentDate)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime");
            entity.Property(e => e.PaymentMethod)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.TransactionId)
                .HasMaxLength(100)
                .IsUnicode(false)
                .HasColumnName("TransactionID");

            entity.HasOne(d => d.Booking).WithMany(p => p.Payments)
                .HasForeignKey(d => d.BookingId)
                .HasConstraintName("FK__Payments__Bookin__7E37BEF6");
        });

        modelBuilder.Entity<Role>(entity =>
        {
            entity.HasKey(e => e.RoleId).HasName("PK__Roles__8AFACE3A3C518649");

            entity.Property(e => e.RoleId).HasColumnName("RoleID");
            entity.Property(e => e.Department).HasMaxLength(100);
            entity.Property(e => e.RoleName).HasMaxLength(100);
            entity.Property(e => e.SalaryGrade)
                .HasMaxLength(10)
                .IsUnicode(false);
        });

        modelBuilder.Entity<Route>(entity =>
        {
            entity.HasKey(e => e.RoutesId).HasName("PK__Routes__6CDC2B23D12F3621");

            entity.Property(e => e.RoutesId).HasColumnName("RoutesID");
            entity.Property(e => e.ArrivalAirportId).HasColumnName("ArrivalAirportID");
            entity.Property(e => e.DepartureAirportId).HasColumnName("DepartureAirportID");
            entity.Property(e => e.Distance).HasColumnType("decimal(10, 2)");

            entity.HasOne(d => d.ArrivalAirport).WithMany(p => p.RouteArrivalAirports)
                .HasForeignKey(d => d.ArrivalAirportId)
                .HasConstraintName("FK__Routes__ArrivalA__5BE2A6F2");

            entity.HasOne(d => d.DepartureAirport).WithMany(p => p.RouteDepartureAirports)
                .HasForeignKey(d => d.DepartureAirportId)
                .HasConstraintName("FK__Routes__Departur__5AEE82B9");
        });

        modelBuilder.Entity<Staff>(entity =>
        {
            entity.HasKey(e => e.StaffId).HasName("PK__Staff__96D4AAF7D51F9103");

            entity.HasIndex(e => e.Email, "UQ__Staff__A9D10534BF74D342").IsUnique();

            entity.Property(e => e.StaffId).HasColumnName("StaffID");
            entity.Property(e => e.Email)
                .HasMaxLength(150)
                .IsUnicode(false);
            entity.Property(e => e.EmploymentStatus)
                .HasMaxLength(20)
                .IsUnicode(false);
            entity.Property(e => e.FirstName).HasMaxLength(100);
            entity.Property(e => e.LastName).HasMaxLength(100);
            entity.Property(e => e.Phone)
                .HasMaxLength(20)
                .IsUnicode(false);
            entity.Property(e => e.RoleId).HasColumnName("RoleID");

            entity.HasOne(d => d.Role).WithMany(p => p.Staff)
                .HasForeignKey(d => d.RoleId)
                .HasConstraintName("FK__Staff__RoleID__5FB337D6");
        });

        modelBuilder.Entity<TechnicalCertification>(entity =>
        {
            entity.HasKey(e => e.CertificationId).HasName("PK__Technica__1237E5AA3BE3C4AB");

            entity.Property(e => e.CertificationId).HasColumnName("CertificationID");
            entity.Property(e => e.LicenseType)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.ModelId).HasColumnName("ModelID");
            entity.Property(e => e.StaffId).HasColumnName("StaffID");

            entity.HasOne(d => d.Model).WithMany(p => p.TechnicalCertifications)
                .HasForeignKey(d => d.ModelId)
                .HasConstraintName("FK__Technical__Model__6A30C649");

            entity.HasOne(d => d.Staff).WithMany(p => p.TechnicalCertifications)
                .HasForeignKey(d => d.StaffId)
                .HasConstraintName("FK__Technical__Staff__693CA210");
        });

        modelBuilder.Entity<Ticket>(entity =>
        {
            entity.HasKey(e => e.TicketId).HasName("PK__Tickets__712CC6276C6564D9");

            entity.HasIndex(e => e.TicketNumber, "UQ__Tickets__CBED06DAC3F4A3EA").IsUnique();

            entity.Property(e => e.TicketId).HasColumnName("TicketID");
            entity.Property(e => e.BookingId).HasColumnName("BookingID");
            entity.Property(e => e.ClassType)
                .HasMaxLength(20)
                .IsUnicode(false);
            entity.Property(e => e.FareId).HasColumnName("FareID");
            entity.Property(e => e.FlightId).HasColumnName("FlightID");
            entity.Property(e => e.IsCheckedIn).HasDefaultValue(false);
            entity.Property(e => e.PassengerId).HasColumnName("PassengerID");
            entity.Property(e => e.SeatNumber)
                .HasMaxLength(5)
                .IsUnicode(false);
            entity.Property(e => e.TicketNumber)
                .HasMaxLength(20)
                .IsUnicode(false);

            entity.HasOne(d => d.Booking).WithMany(p => p.Tickets)
                .HasForeignKey(d => d.BookingId)
                .HasConstraintName("FK__Tickets__Booking__787EE5A0");

            entity.HasOne(d => d.Fare).WithMany(p => p.Tickets)
                .HasForeignKey(d => d.FareId)
                .HasConstraintName("FK__Tickets__FareID__797309D9");

            entity.HasOne(d => d.Flight).WithMany(p => p.Tickets)
                .HasForeignKey(d => d.FlightId)
                .HasConstraintName("FK__Tickets__FlightI__76969D2E");

            entity.HasOne(d => d.Passenger).WithMany(p => p.Tickets)
                .HasForeignKey(d => d.PassengerId)
                .HasConstraintName("FK__Tickets__Passeng__778AC167");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
