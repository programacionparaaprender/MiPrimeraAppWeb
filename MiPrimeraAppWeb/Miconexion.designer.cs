﻿#pragma warning disable 1591
//------------------------------------------------------------------------------
// <auto-generated>
//     Este código fue generado por una herramienta.
//     Versión de runtime:4.0.30319.42000
//
//     Los cambios en este archivo podrían causar un comportamiento incorrecto y se perderán si
//     se vuelve a generar el código.
// </auto-generated>
//------------------------------------------------------------------------------

namespace MiPrimeraAppWeb
{
	using System.Data.Linq;
	using System.Data.Linq.Mapping;
	using System.Data;
	using System.Collections.Generic;
	using System.Reflection;
	using System.Linq;
	using System.Linq.Expressions;
	using System.ComponentModel;
	using System;
	
	
	[global::System.Data.Linq.Mapping.DatabaseAttribute(Name="SistemaMatricula")]
	public partial class MiconexionDataContext : System.Data.Linq.DataContext
	{
		
		private static System.Data.Linq.Mapping.MappingSource mappingSource = new AttributeMappingSource();
		
    #region Definiciones de métodos de extensibilidad
    partial void OnCreated();
    partial void InsertCurso(Curso instance);
    partial void UpdateCurso(Curso instance);
    partial void DeleteCurso(Curso instance);
    partial void InsertCurso1(Curso1 instance);
    partial void UpdateCurso1(Curso1 instance);
    partial void DeleteCurso1(Curso1 instance);
    #endregion
		
		public MiconexionDataContext() : 
				base(global::System.Configuration.ConfigurationManager.ConnectionStrings["SistemaMatriculaConnectionString"].ConnectionString, mappingSource)
		{
			OnCreated();
		}
		
		public MiconexionDataContext(string connection) : 
				base(connection, mappingSource)
		{
			OnCreated();
		}
		
		public MiconexionDataContext(System.Data.IDbConnection connection) : 
				base(connection, mappingSource)
		{
			OnCreated();
		}
		
		public MiconexionDataContext(string connection, System.Data.Linq.Mapping.MappingSource mappingSource) : 
				base(connection, mappingSource)
		{
			OnCreated();
		}
		
		public MiconexionDataContext(System.Data.IDbConnection connection, System.Data.Linq.Mapping.MappingSource mappingSource) : 
				base(connection, mappingSource)
		{
			OnCreated();
		}
		
		public System.Data.Linq.Table<Curso> Curso
		{
			get
			{
				return this.GetTable<Curso>();
			}
		}
		
		public System.Data.Linq.Table<Curso1> Curso1
		{
			get
			{
				return this.GetTable<Curso1>();
			}
		}
	}
	
	[global::System.Data.Linq.Mapping.TableAttribute(Name="dbo.Curso")]
	public partial class Curso : INotifyPropertyChanging, INotifyPropertyChanged
	{
		
		private static PropertyChangingEventArgs emptyChangingEventArgs = new PropertyChangingEventArgs(String.Empty);
		
		private int _IIDCURSO;
		
		private string _NOMBRE;
		
		private string _DESCRIPCION;
		
		private System.Nullable<int> _BHABILITADO;
		
    #region Definiciones de métodos de extensibilidad
    partial void OnLoaded();
    partial void OnValidate(System.Data.Linq.ChangeAction action);
    partial void OnCreated();
    partial void OnIIDCURSOChanging(int value);
    partial void OnIIDCURSOChanged();
    partial void OnNOMBREChanging(string value);
    partial void OnNOMBREChanged();
    partial void OnDESCRIPCIONChanging(string value);
    partial void OnDESCRIPCIONChanged();
    partial void OnBHABILITADOChanging(System.Nullable<int> value);
    partial void OnBHABILITADOChanged();
    #endregion
		
		public Curso()
		{
			OnCreated();
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_IIDCURSO", AutoSync=AutoSync.OnInsert, DbType="Int NOT NULL IDENTITY", IsPrimaryKey=true, IsDbGenerated=true)]
		public int IIDCURSO
		{
			get
			{
				return this._IIDCURSO;
			}
			set
			{
				if ((this._IIDCURSO != value))
				{
					this.OnIIDCURSOChanging(value);
					this.SendPropertyChanging();
					this._IIDCURSO = value;
					this.SendPropertyChanged("IIDCURSO");
					this.OnIIDCURSOChanged();
				}
			}
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_NOMBRE", DbType="VarChar(100)")]
		public string NOMBRE
		{
			get
			{
				return this._NOMBRE;
			}
			set
			{
				if ((this._NOMBRE != value))
				{
					this.OnNOMBREChanging(value);
					this.SendPropertyChanging();
					this._NOMBRE = value;
					this.SendPropertyChanged("NOMBRE");
					this.OnNOMBREChanged();
				}
			}
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_DESCRIPCION", DbType="VarChar(200)")]
		public string DESCRIPCION
		{
			get
			{
				return this._DESCRIPCION;
			}
			set
			{
				if ((this._DESCRIPCION != value))
				{
					this.OnDESCRIPCIONChanging(value);
					this.SendPropertyChanging();
					this._DESCRIPCION = value;
					this.SendPropertyChanged("DESCRIPCION");
					this.OnDESCRIPCIONChanged();
				}
			}
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_BHABILITADO", DbType="Int")]
		public System.Nullable<int> BHABILITADO
		{
			get
			{
				return this._BHABILITADO;
			}
			set
			{
				if ((this._BHABILITADO != value))
				{
					this.OnBHABILITADOChanging(value);
					this.SendPropertyChanging();
					this._BHABILITADO = value;
					this.SendPropertyChanged("BHABILITADO");
					this.OnBHABILITADOChanged();
				}
			}
		}
		
		public event PropertyChangingEventHandler PropertyChanging;
		
		public event PropertyChangedEventHandler PropertyChanged;
		
		protected virtual void SendPropertyChanging()
		{
			if ((this.PropertyChanging != null))
			{
				this.PropertyChanging(this, emptyChangingEventArgs);
			}
		}
		
		protected virtual void SendPropertyChanged(String propertyName)
		{
			if ((this.PropertyChanged != null))
			{
				this.PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
			}
		}
	}
	
	[global::System.Data.Linq.Mapping.TableAttribute(Name="dbo.Curso")]
	public partial class Curso1 : INotifyPropertyChanging, INotifyPropertyChanged
	{
		
		private static PropertyChangingEventArgs emptyChangingEventArgs = new PropertyChangingEventArgs(String.Empty);
		
		private int _IIDCURSO;
		
		private string _NOMBRE;
		
		private string _DESCRIPCION;
		
		private System.Nullable<int> _BHABILITADO;
		
    #region Definiciones de métodos de extensibilidad
    partial void OnLoaded();
    partial void OnValidate(System.Data.Linq.ChangeAction action);
    partial void OnCreated();
    partial void OnIIDCURSOChanging(int value);
    partial void OnIIDCURSOChanged();
    partial void OnNOMBREChanging(string value);
    partial void OnNOMBREChanged();
    partial void OnDESCRIPCIONChanging(string value);
    partial void OnDESCRIPCIONChanged();
    partial void OnBHABILITADOChanging(System.Nullable<int> value);
    partial void OnBHABILITADOChanged();
    #endregion
		
		public Curso1()
		{
			OnCreated();
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_IIDCURSO", AutoSync=AutoSync.OnInsert, DbType="Int NOT NULL IDENTITY", IsPrimaryKey=true, IsDbGenerated=true)]
		public int IIDCURSO
		{
			get
			{
				return this._IIDCURSO;
			}
			set
			{
				if ((this._IIDCURSO != value))
				{
					this.OnIIDCURSOChanging(value);
					this.SendPropertyChanging();
					this._IIDCURSO = value;
					this.SendPropertyChanged("IIDCURSO");
					this.OnIIDCURSOChanged();
				}
			}
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_NOMBRE", DbType="VarChar(100)")]
		public string NOMBRE
		{
			get
			{
				return this._NOMBRE;
			}
			set
			{
				if ((this._NOMBRE != value))
				{
					this.OnNOMBREChanging(value);
					this.SendPropertyChanging();
					this._NOMBRE = value;
					this.SendPropertyChanged("NOMBRE");
					this.OnNOMBREChanged();
				}
			}
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_DESCRIPCION", DbType="VarChar(200)")]
		public string DESCRIPCION
		{
			get
			{
				return this._DESCRIPCION;
			}
			set
			{
				if ((this._DESCRIPCION != value))
				{
					this.OnDESCRIPCIONChanging(value);
					this.SendPropertyChanging();
					this._DESCRIPCION = value;
					this.SendPropertyChanged("DESCRIPCION");
					this.OnDESCRIPCIONChanged();
				}
			}
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_BHABILITADO", DbType="Int")]
		public System.Nullable<int> BHABILITADO
		{
			get
			{
				return this._BHABILITADO;
			}
			set
			{
				if ((this._BHABILITADO != value))
				{
					this.OnBHABILITADOChanging(value);
					this.SendPropertyChanging();
					this._BHABILITADO = value;
					this.SendPropertyChanged("BHABILITADO");
					this.OnBHABILITADOChanged();
				}
			}
		}
		
		public event PropertyChangingEventHandler PropertyChanging;
		
		public event PropertyChangedEventHandler PropertyChanged;
		
		protected virtual void SendPropertyChanging()
		{
			if ((this.PropertyChanging != null))
			{
				this.PropertyChanging(this, emptyChangingEventArgs);
			}
		}
		
		protected virtual void SendPropertyChanged(String propertyName)
		{
			if ((this.PropertyChanged != null))
			{
				this.PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
			}
		}
	}
}
#pragma warning restore 1591
