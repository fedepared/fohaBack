using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace Foha.Models
{
    public partial class fohaContext : DbContext
    {
        public fohaContext()
        {
        }

        public fohaContext(DbContextOptions<fohaContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Cliente> Cliente { get; set; }
        public virtual DbSet<Colores> Colores { get; set; }
        public virtual DbSet<Empleado> Empleado { get; set; }
        public virtual DbSet<Etapa> Etapa { get; set; }
        public virtual DbSet<Rango> Rango { get; set; }
        public virtual DbSet<Sectores> Sectores { get; set; }
        public virtual DbSet<TipoEtapa> TipoEtapa { get; set; }
        public virtual DbSet<TipoTransfo> TipoTransfo { get; set; }
        public virtual DbSet<Transformadores> Transformadores { get; set; }
        public virtual DbSet<Usuario> Usuario { get; set; }

        public virtual DbSet<EtapaEmpleado> EtapaEmpleado {get;set;}
        public virtual DbSet<MegaUltraArchiVista> MegaUltraArchiVista {get;set;}

        public virtual DbSet<VistaTiemposParc> VistaTiemposParc{get;set;}

        // Unable to generate entity type for table 'dbo.EtapaEmpleado'. Please see the warning messages.
        // Unable to generate entity type for table 'dbo.tipoEtSubEt'. Please see the warning messages.
        // Unable to generate entity type for table 'dbo.transfoEtap'. Please see the warning messages.

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. See http://go.microsoft.com/fwlink/?LinkId=723263 for guidance on storing connection strings.
                optionsBuilder.UseSqlServer("Server=FEDERICO-PC;Database=foha;Trusted_Connection=True;User Id=sa;Password=102401;Integrated Security=false;");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {

            modelBuilder.Entity<MegaUltraArchiVista>(entity=>{
                
                
                entity.HasKey(e => e.IdTransfo);
                entity.Property(e=>e.IdTransfo)
                .HasColumnName("idTransfo")
                
                .ValueGeneratedNever();
                entity.Property(e=>e.OPe)
                .HasColumnName("oPe");                
                entity.Property(e=>e.OTe)
                .HasColumnName("oTe");                
                entity.Property(e=>e.Observaciones)
                .HasColumnName("observaciones");                
                entity.Property(e=>e.RangoInicio)
                .HasColumnName("rangoInicio");                
                entity.Property(e=>e.RangoFin)
                .HasColumnName("rangoFin");                 
                entity.Property(e=>e.NombreCli)
                .HasColumnName("nombreCli");
                entity.Property(e=>e.Potencia)
                .HasColumnName("potencia");
                entity.Property(e=>e.Documentacion)
                .HasColumnName("documentacion");
                entity.Property(e=>e.BobinaBT1)
                .HasColumnName("bobinaBT1");
                entity.Property(e=>e.BobinaBT2)
                .HasColumnName("bobinaBT2");
                entity.Property(e=>e.BobinaBT3)
                .HasColumnName("bobinaBT3");
                entity.Property(e=>e.BobinaAT1)
                .HasColumnName("bobinaAT1");
                entity.Property(e=>e.BobinaAT2)
                .HasColumnName("bobinaAT2");
                entity.Property(e=>e.BobinaAT3)
                .HasColumnName("bobinaAT3");
                entity.Property(e=>e.BobinaRG1)
                .HasColumnName("bobinaRG1");
                entity.Property(e=>e.BobinaRG2)
                .HasColumnName("bobinaRG2");
                entity.Property(e=>e.BobinaRG3)
                .HasColumnName("bobinaRG3");
                entity.Property(e=>e.BobinaRF1)
                .HasColumnName("bobinaRF1");
                entity.Property(e=>e.BobinaRF2)
                .HasColumnName("bobinaRF2");
                entity.Property(e=>e.BobinaRF3)
                .HasColumnName("bobinaRF3");
                entity.Property(e=>e.EnsamblajeBobinas)
                .HasColumnName("ensamblajeBobinas");
                entity.Property(e=>e.CorteYPlegadoPYS)
                .HasColumnName("corteYPlegadoPYS");
                entity.Property(e=>e.SoldaduraPYS)
                .HasColumnName("soldaduraPYS");
                entity.Property(e=>e.EnvioPYS)
                .HasColumnName("envioPYS");
                entity.Property(e=>e.Nucleo)
                .HasColumnName("nucleo");
                entity.Property(e=>e.Montaje)
                .HasColumnName("montaje");
                entity.Property(e=>e.Horno)
                .HasColumnName("horno");
                entity.Property(e=>e.CYPTapaCuba)
                .HasColumnName("cYPTapaCuba");
                entity.Property(e=>e.Tapa)
                .HasColumnName("tapa");
                entity.Property(e=>e.RadiadoresOPaneles)
                .HasColumnName("radiadoresOPaneles");
                entity.Property(e=>e.Cuba)
                .HasColumnName("cuba");
                entity.Property(e=>e.TintasPenetrantes)
                .HasColumnName("tintasPenetrantes");
                entity.Property(e=>e.Granallado)
                .HasColumnName("granallado");
                entity.Property(e=>e.Pintura)
                .HasColumnName("pintura");
                entity.Property(e=>e.Encubado)
                .HasColumnName("encubado");
                entity.Property(e=>e.EnsayosRef)
                .HasColumnName("ensayosRef");
                entity.Property(e=>e.Terminacion)
                .HasColumnName("terminacion");
                entity.Property(e=>e.EnvioADeposito)
                .HasColumnName("envioADeposito");
                entity.Property(e=>e.EnvioACliente)
                .HasColumnName("envioACliente");
            });




            modelBuilder.Entity<VistaTiemposParc>(entity=>{
                entity.HasKey(e => e.IdTransfo);
                entity.Property(e=>e.IdTransfo)
                .HasColumnName("idTransfo")
                
                .ValueGeneratedNever();
                entity.Property(e=>e.OPe)
                .HasColumnName("oPe");
                entity.Property(e=>e.OTe)
                .HasColumnName("oTe");                
                entity.Property(e=>e.Observaciones)
                .HasColumnName("observaciones");               
                entity.Property(e=>e.RangoInicio)
                .HasColumnName("rangoInicio");
                entity.Property(e=>e.RangoFin)
                .HasColumnName("rangoFin");                                
                entity.Property(e=>e.IdCliente)
                .HasColumnName("idCliente");                
                entity.Property(e=>e.NombreCli)
                .HasColumnName("nombreCli");                
                entity.Property(e=>e.Potencia)
                .HasColumnName("potencia");
                entity.Property(e=>e.IdTipoTransfo)
                .HasColumnName("idTipoTransfo");
                entity.Property(e=>e.FechaDeCreacion)
                .HasColumnName("fechaCreacion");
                entity.Property(e=>e.Anio)
                .HasColumnName("anio");
                entity.Property(e=>e.Mes)
                .HasColumnName("mes");
                entity.Property(e=>e.Prioridad)
                .HasColumnName("prioridad");
                entity.Property(e=>e.Documentacion)
                .HasColumnName("documentacion");
                entity.Property(e=>e.DocTiempoParc)
                .HasColumnName("docTiempoParc");
                entity.Property(e=>e.BobinaBT1)
                .HasColumnName("bobinaBT1");
                entity.Property(e=>e.BBT1TiempoParc)
                .HasColumnName("bBT1TiempoParc");
                entity.Property(e=>e.BobinaBT2)
                .HasColumnName("bobinaBT2");
                entity.Property(e=>e.BBT2TiempoParc)
                .HasColumnName("bBT2TiempoParc");
                entity.Property(e=>e.BobinaBT3)
                .HasColumnName("bobinaBT3");
                entity.Property(e=>e.BBT3TiempoParc)
                .HasColumnName("bBT3TiempoParc");
                entity.Property(e=>e.BobinaAT1)
                .HasColumnName("bobinaAT1");
                entity.Property(e=>e.BAT1TiempoParc)
                .HasColumnName("bAT1TiempoParc");
                entity.Property(e=>e.BobinaAT2)
                .HasColumnName("bobinaAT2");
                entity.Property(e=>e.BAT2TiempoParc)
                .HasColumnName("bAT2TiempoParc");
                entity.Property(e=>e.BobinaAT3)
                .HasColumnName("bobinaAT3");
                entity.Property(e=>e.BAT3TiempoParc)
                .HasColumnName("bAT3TiempoParc");
                entity.Property(e=>e.BRG1TiempoParc)
                .HasColumnName("bRG1TiempoParc");
                entity.Property(e=>e.BobinaRG2)
                .HasColumnName("bobinaRG2");
                entity.Property(e=>e.BRG2TiempoParc)
                .HasColumnName("bRG2TiempoParc");
                entity.Property(e=>e.BobinaRG3)
                .HasColumnName("bobinaRG3");
                entity.Property(e=>e.BRG3TiempoParc)
                .HasColumnName("bRG3TiempoParc");
                entity.Property(e=>e.BobinaRF1)
                .HasColumnName("bobinaRF1");
                entity.Property(e=>e.BRF1TiempoParc)
                .HasColumnName("bRF1TiempoParc");
                entity.Property(e=>e.BobinaRF2)
                .HasColumnName("bobinaRF2");
                entity.Property(e=>e.BRF2TiempoParc)
                .HasColumnName("bRF2TiempoParc");
                entity.Property(e=>e.BobinaRF3)
                .HasColumnName("bobinaRF3");
                entity.Property(e=>e.BRF3TiempoParc)
                .HasColumnName("bRF3TiempoParc");
                entity.Property(e=>e.EnsamblajeBobinas)
                .HasColumnName("ensamblajeBobinas");    
                entity.Property(e=>e.EnsBobTiempoParc)
                .HasColumnName("ensBobTiempoParc");    
                entity.Property(e=>e.CorteYPlegadoPYS)
                .HasColumnName("corteYPlegadoPYS");
                entity.Property(e=>e.CYPTiempoParc)
                .HasColumnName("cYPTiempoParc");
                entity.Property(e=>e.SoldaduraPYS)
                .HasColumnName("soldaduraPYS");    
                entity.Property(e=>e.SolTiempoParc)
                .HasColumnName("solTiempoParc");
                entity.Property(e=>e.EnvioPYS)
                .HasColumnName("envioPYS");
                entity.Property(e=>e.EnvTiempoParc)
                .HasColumnName("envTiempoParc");
                entity.Property(e=>e.Nucleo)
                .HasColumnName("nucleo");
                entity.Property(e=>e.NucleoTiempoParc)
                .HasColumnName("nucleoTiempoParc");
                entity.Property(e=>e.Montaje)
                .HasColumnName("montaje");
                entity.Property(e=>e.MontajeTiempoParc)
                .HasColumnName("montajeTiempoParc");
                entity.Property(e=>e.Horno)
                .HasColumnName("horno");
                entity.Property(e=>e.HornoTiempoParc)
                .HasColumnName("hornoTiempoParc");
                entity.Property(e=>e.CYPTapaCuba)
                .HasColumnName("cYPTapaCuba");
                entity.Property(e=>e.CYPTCTiempoParc)
                .HasColumnName("cYPTCTiempoParc");
                entity.Property(e=>e.Tapa)
                .HasColumnName("tapa");
                entity.Property(e=>e.TapaTiempoParc)
                .HasColumnName("tapaTiempoParc");
                entity.Property(e=>e.RadiadoresOPaneles)
                .HasColumnName("radiadoresOPaneles");
                entity.Property(e=>e.ROPTiempoParc)
                .HasColumnName("rOPTiempoParc");
                entity.Property(e=>e.Cuba)
                .HasColumnName("cuba");
                entity.Property(e=>e.CubaTiempoParc)
                .HasColumnName("cubaTiempoParc");
                entity.Property(e=>e.TintasPenetrantes)
                .HasColumnName("tintasPenetrantes");
                entity.Property(e=>e.TPTiempoParc)
                .HasColumnName("tPTiempoParc");
                entity.Property(e=>e.Granallado)
                .HasColumnName("granallado");
                entity.Property(e=>e.GranalladoTiempoParc)
                .HasColumnName("granalladoTiempoParc");
                entity.Property(e=>e.Pintura)
                .HasColumnName("pintura");
                entity.Property(e=>e.PinturaTiempoParc)
                .HasColumnName("pinturaTiempoParc");
                entity.Property(e=>e.Encubado)
                .HasColumnName("encubado");
                entity.Property(e=>e.EncubadoTiempoParc)
                .HasColumnName("encubadoTiempoParc");
                entity.Property(e=>e.EnsayosRef)
                .HasColumnName("ensayosRef");
                entity.Property(e=>e.ERefTiempoParc)
                .HasColumnName("eRefTiempoParc");
                entity.Property(e=>e.Terminacion)
                .HasColumnName("terminacion");
                entity.Property(e=>e.TerminacionTiempoParc)
                .HasColumnName("terminacionTiempoParc");
                entity.Property(e=>e.EnvioADeposito)
                .HasColumnName("envioADeposito");
                entity.Property(e=>e.EADepTiempoParc)
                .HasColumnName("eADepTiempoParc");
                entity.Property(e=>e.EnvioACliente)
                .HasColumnName("envioACliente");
                entity.Property(e=>e.EACliTiempoParc)
                .HasColumnName("eACliTiempoParc");

            });

            modelBuilder.Entity<Cliente>(entity =>
            {
                entity.HasKey(e => e.IdCliente);

                entity.Property(e => e.IdCliente).HasColumnName("idCliente");

                entity.Property(e => e.NombreCli)
                    .IsRequired()
                    .HasColumnName("nombreCli")
                    .HasMaxLength(150)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<Colores>(entity =>
            {
                entity.HasKey(e => e.IdColor);

                entity.Property(e => e.IdColor).HasColumnName("idColor");

                entity.Property(e => e.CodigoColor)
                    .IsRequired()
                    .HasColumnName("codigoColor")
                    .HasMaxLength(50)
                    .IsUnicode(false);
                entity.Property(e => e.Leyenda)
                .IsRequired()
                .HasColumnName("leyenda")
                .HasMaxLength(200)
                .IsUnicode(false);
            });

            modelBuilder.Entity<Empleado>(entity =>
            {
                entity.HasKey(e => e.IdEmpleado);

                entity.Property(e => e.IdEmpleado)
                    .HasColumnName("idEmpleado")
                    .ValueGeneratedNever()
                    .HasMaxLength(50);

                entity.Property(e => e.NombreEmp)
                    .IsRequired()
                    .HasColumnName("nombreEmp")
                    .HasMaxLength(150)
                    .IsUnicode(false);
                entity.Property(e => e.IdSector)
                    .HasColumnName("idSector");
                
                entity.HasOne(d => d.Sector)
                    .WithMany(p => p.Empleado)
                    .HasForeignKey(d => d.IdSector)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Empleado_Sectores");
                


            });

            modelBuilder.Entity<Etapa>(entity =>
            {
                entity.HasKey(e => e.IdEtapa);

                entity.Property(e => e.IdEtapa).HasColumnName("idEtapa");

                entity.Property(e => e.DateFin)
                    .HasColumnName("dateFin")
                    .HasColumnType("datetime");

                entity.Property(e => e.DateIni)
                    .HasColumnName("dateIni")
                    .HasColumnType("datetime");

                entity.Property(e => e.Hora)
                    .HasColumnName("hora")
                    .HasMaxLength(10);

                entity.Property(e => e.IdColor).HasColumnName("idColor");

                entity.Property(e => e.IdEmpleado).HasColumnName("idEmpleado");

                entity.Property(e => e.IdTipoEtapa).HasColumnName("idTipoEtapa");

                entity.Property(e => e.IdTransfo).HasColumnName("idTransfo");

                entity.Property(e => e.InicioProceso)
                    .HasColumnName("inicioProceso")
                    .HasColumnType("datetime");

                entity.Property(e => e.IsEnded).HasColumnName("isEnded");

                entity.Property(e => e.TiempoFin)
                    .HasColumnName("tiempoFin")
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.TiempoParc)
                    .HasColumnName("tiempoParc")
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.HasOne(d => d.IdColorNavigation)
                    .WithMany(p => p.Etapa)
                    .HasForeignKey(d => d.IdColor)
                    .HasConstraintName("FK_Etapa_Colores");

                entity.HasOne(d => d.IdTipoEtapaNavigation)
                    .WithMany(p => p.Etapa)
                    .HasForeignKey(d => d.IdTipoEtapa)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Etapa_tipoEtapa");

                entity.HasOne(d => d.IdTransfoNavigation)
                    .WithMany(p => p.Etapa)
                    .HasForeignKey(d => d.IdTransfo)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Etapa_Transformadores");
            });

            modelBuilder.Entity<EtapaEmpleado>(entity=>
            {
                entity.HasKey(e=>new {e.IdEtapa,e.IdEmpleado});
                entity.Property(e => e.IdEmpleado).HasColumnName("idEmpleado");
                entity.Property(e => e.IdEtapa).HasColumnName("idEtapa");
                entity.Property(e => e.DateIni).HasColumnName("dateIni");
                entity.Property(e => e.DateFin).HasColumnName("dateFin");
                entity.Property(e => e.TiempoParc).HasColumnName("tiempoParc");
                entity.Property(e => e.TiempoFin).HasColumnName("tiempoFin");
                entity.Property(e => e.IsEnded).HasColumnName("isEnded");

            });

            modelBuilder.Entity<EtapaEmpleado>()
                .HasOne(m => m.Empleado)
                .WithMany(ma => ma.EtapaEmpleado)
                .HasForeignKey(m => m.IdEmpleado);

            modelBuilder.Entity<EtapaEmpleado>()
                .HasOne(m => m.Etapa)
                .WithMany(ma => ma.EtapaEmpleado)
                .HasForeignKey(a => a.IdEtapa);


            modelBuilder.Entity<Rango>(entity =>
            {
                entity.HasKey(e => e.IdRango);

                entity.Property(e => e.IdRango).HasColumnName("idRango");

                entity.Property(e => e.Cantidad).HasColumnName("cantidad");
            });

            modelBuilder.Entity<Sectores>(entity =>
            {
                entity.HasKey(e => e.IdSector);

                entity.Property(e => e.IdSector).HasColumnName("idSector");

                entity.Property(e => e.NombreSector)
                    .IsRequired()
                    .HasColumnName("nombreSector")
                    .HasMaxLength(20)
                    .IsUnicode(false);
            });


            modelBuilder.Entity<TipoEtapa>(entity =>
            {
                entity.HasKey(e => e.IdTipoEtapa);

                entity.ToTable("tipoEtapa");

                entity.Property(e => e.IdTipoEtapa).HasColumnName("idTipoEtapa");

                entity.Property(e => e.NombreEtapa)
                    .IsRequired()
                    .HasColumnName("nombreEtapa")
                    .HasMaxLength(150)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<TipoTransfo>(entity =>
            {
                entity.HasKey(e => e.IdTipoTransfo);

                entity.Property(e => e.IdTipoTransfo).HasColumnName("idTipoTransfo");

                entity.Property(e => e.NombreTipoTransfo)
                    .IsRequired()
                    .HasColumnName("nombreTipoTransfo")
                    .HasMaxLength(20)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<Transformadores>(entity =>
            {
                entity.HasKey(e => e.IdTransfo);

                entity.Property(e => e.IdTransfo).HasColumnName("idTransfo");

                entity.Property(e => e.Anio).HasColumnName("anio");

                entity.Property(e => e.FechaCreacion)
                    .HasColumnName("fechaCreacion")
                    .HasColumnType("datetime");

                entity.Property(e => e.IdCliente).HasColumnName("idCliente");

                entity.Property(e => e.IdTipoTransfo).HasColumnName("idTipoTransfo");

                entity.Property(e => e.Mes).HasColumnName("mes");

                entity.Property(e => e.NombreCli)
                    .HasColumnName("nombreCli")
                    .HasMaxLength(150)
                    .IsUnicode(false);

                entity.Property(e => e.OPe)
                    .IsRequired()
                    .HasColumnName("oPe")
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.OTe).HasColumnName("oTe");

                entity.Property(e => e.Observaciones)
                    .HasColumnName("observaciones")
                    .HasMaxLength(200)
                    .IsUnicode(false);

                entity.Property(e => e.Potencia).HasColumnName("potencia");

                entity.Property(e => e.Prioridad).HasColumnName("prioridad");

                entity.Property(e => e.RangoFin).HasColumnName("rangoFin");

                entity.Property(e => e.RangoInicio).HasColumnName("rangoInicio");

                entity.HasOne(d => d.IdClienteNavigation)
                    .WithMany(p => p.Transformadores)
                    .HasForeignKey(d => d.IdCliente)
                    .HasConstraintName("FK_Transformadores_Cliente");

                entity.HasOne(d => d.IdTipoTransfoNavigation)
                    .WithMany(p => p.Transformadores)
                    .HasForeignKey(d => d.IdTipoTransfo)
                    .HasConstraintName("FK_Transformadores_TipoTransfo");
            });

            modelBuilder.Entity<Usuario>(entity =>
            {
                entity.HasKey(e => e.IdUser);

                entity.Property(e => e.IdUser).HasColumnName("idUser");

                entity.Property(e => e.IdSector).HasColumnName("idSector");

                entity.Property(e => e.IsOp).HasColumnName("isOp");

                entity.Property(e => e.NombreUs)
                    .IsRequired()
                    .HasColumnName("nombreUs")
                    .HasMaxLength(150)
                    .IsUnicode(false);

                entity.Property(e => e.Pass)
                    .IsRequired()
                    .HasColumnName("pass")
                    .HasMaxLength(128);

                entity.Property(e => e.Salt)
                    .IsRequired()
                    .HasColumnName("salt")
                    .HasMaxLength(128);

                entity.Property(e => e.RefreshToken)
                    .HasColumnName("refreshToken")
                    .HasMaxLength(150);
                


                entity.HasOne(d => d.IdSectorNavigation)
                    .WithMany(p => p.Usuario)
                    .HasForeignKey(d => d.IdSector)
                    .HasConstraintName("FK_Usuario_Sectores");
            });
        }
    }
}
