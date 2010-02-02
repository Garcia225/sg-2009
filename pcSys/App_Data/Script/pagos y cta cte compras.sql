/*
 * ER/Studio 7.1 SQL Code Generation
 * Company :      Vale
 * Project :      Pagos y cta. cte. compras 3.DM1
 * Author :       Vale
 *
 * Date Created : Tuesday, November 03, 2009 14:29:52
 * Target DBMS : Microsoft SQL Server 2005
 */

/* 
 * TABLE: PCCC_BANCO 
 */

CREATE TABLE PCCC_BANCO(
    id_banco     int            IDENTITY(1,1),
    banco        varchar(20)    NOT NULL,
    num_cta      varchar(20)    NOT NULL,
    direccion    varchar(40)    NOT NULL,
    CONSTRAINT PK26 PRIMARY KEY NONCLUSTERED (id_banco)
)
go



IF OBJECT_ID('PCCC_BANCO') IS NOT NULL
    PRINT '<<< CREATED TABLE PCCC_BANCO >>>'
ELSE
    PRINT '<<< FAILED CREATING TABLE PCCC_BANCO >>>'
go

/* 
 * TABLE: PCCC_CHEQUE 
 */

CREATE TABLE PCCC_CHEQUE(
    id_cheque           int            IDENTITY(1,1),
    importe             int            NULL,
    num_cheque          varchar(30)    NULL,
    fecha               datetime       NOT NULL,
    id_orden_de_pago    int            NOT NULL,
    id_banco            int            NOT NULL,
    CONSTRAINT PK11 PRIMARY KEY NONCLUSTERED (id_cheque)
)
go



IF OBJECT_ID('PCCC_CHEQUE') IS NOT NULL
    PRINT '<<< CREATED TABLE PCCC_CHEQUE >>>'
ELSE
    PRINT '<<< FAILED CREATING TABLE PCCC_CHEQUE >>>'
go

/* 
 * TABLE: PCCC_CONDICION_DE_PAGO 
 */

CREATE TABLE PCCC_CONDICION_DE_PAGO(
    id_condicion_de_pago    int            IDENTITY(1,1),
    descripcion             varchar(20)    NULL,
    CONSTRAINT PK5 PRIMARY KEY NONCLUSTERED (id_condicion_de_pago)
)
go



IF OBJECT_ID('PCCC_CONDICION_DE_PAGO') IS NOT NULL
    PRINT '<<< CREATED TABLE PCCC_CONDICION_DE_PAGO >>>'
ELSE
    PRINT '<<< FAILED CREATING TABLE PCCC_CONDICION_DE_PAGO >>>'
go

/* 
 * TABLE: PCCC_CTA_CTE_PROVEEDOR 
 */

CREATE TABLE PCCC_CTA_CTE_PROVEEDOR(
    id_cta_cte_proveedor    int      IDENTITY(1,1),
    id_proveedor            int      NULL,
    debe                    float    NOT NULL,
    haber                   float    NOT NULL,
    saldo                   float    NULL,
    estado                  text     NOT NULL,
    CONSTRAINT PK29 PRIMARY KEY NONCLUSTERED (id_cta_cte_proveedor)
)
go



IF OBJECT_ID('PCCC_CTA_CTE_PROVEEDOR') IS NOT NULL
    PRINT '<<< CREATED TABLE PCCC_CTA_CTE_PROVEEDOR >>>'
ELSE
    PRINT '<<< FAILED CREATING TABLE PCCC_CTA_CTE_PROVEEDOR >>>'
go

/* 
 * TABLE: PCCC_CUPON_DE_CREDITO 
 */

CREATE TABLE PCCC_CUPON_DE_CREDITO(
    id_nota_debito      int         IDENTITY(1,1),
    num_tarjeta         int         NOT NULL,
    importe             int         NOT NULL,
    fecha               datetime    NULL,
    id_banco            int         NOT NULL,
    id_orden_de_pago    int         NOT NULL,
    CONSTRAINT PK31 PRIMARY KEY NONCLUSTERED (id_nota_debito)
)
go



IF OBJECT_ID('PCCC_CUPON_DE_CREDITO') IS NOT NULL
    PRINT '<<< CREATED TABLE PCCC_CUPON_DE_CREDITO >>>'
ELSE
    PRINT '<<< FAILED CREATING TABLE PCCC_CUPON_DE_CREDITO >>>'
go

/* 
 * TABLE: PCCC_DET_NOTA_CREDITO 
 */

CREATE TABLE PCCC_DET_NOTA_CREDITO(
    num_renglon        int            NOT NULL,
    id_nota_credito    int            NOT NULL,
    estado_prodcuto    varchar(20)    NULL,
    CONSTRAINT PK6_1_1 PRIMARY KEY NONCLUSTERED (num_renglon, id_nota_credito)
)
go



IF OBJECT_ID('PCCC_DET_NOTA_CREDITO') IS NOT NULL
    PRINT '<<< CREATED TABLE PCCC_DET_NOTA_CREDITO >>>'
ELSE
    PRINT '<<< FAILED CREATING TABLE PCCC_DET_NOTA_CREDITO >>>'
go

/* 
 * TABLE: PCCC_DETALLE_FACTURA 
 */

CREATE TABLE PCCC_DETALLE_FACTURA(
    num_renglon          int    IDENTITY(1,1),
    cantidad_recibida    int    NOT NULL,
    precio               int    NULL,
    cantidad             int    NULL,
    id_factura           int    NOT NULL,
    id_materia_prima     int    NOT NULL,
    id_estado            int    NOT NULL,
    CONSTRAINT PK6 PRIMARY KEY NONCLUSTERED (num_renglon)
)
go



IF OBJECT_ID('PCCC_DETALLE_FACTURA') IS NOT NULL
    PRINT '<<< CREATED TABLE PCCC_DETALLE_FACTURA >>>'
ELSE
    PRINT '<<< FAILED CREATING TABLE PCCC_DETALLE_FACTURA >>>'
go

/* 
 * TABLE: PCCC_DETALLE_RECIBO 
 */

CREATE TABLE PCCC_DETALLE_RECIBO(
    item           int             IDENTITY(1,1),
    id_recibo      int             NOT NULL,
    id_factura     int             NOT NULL,
    monto          float           NOT NULL,
    descripcion    varchar(100)    NULL,
    CONSTRAINT PK32 PRIMARY KEY NONCLUSTERED (item, id_recibo)
)
go



IF OBJECT_ID('PCCC_DETALLE_RECIBO') IS NOT NULL
    PRINT '<<< CREATED TABLE PCCC_DETALLE_RECIBO >>>'
ELSE
    PRINT '<<< FAILED CREATING TABLE PCCC_DETALLE_RECIBO >>>'
go

/* 
 * TABLE: PCCC_EMPLEADO 
 */

CREATE TABLE PCCC_EMPLEADO(
    id_empleado    int            IDENTITY(1,1),
    num_doc        int            NOT NULL,
    tipo_doc       varchar(20)    NOT NULL,
    nick           varchar(40)    NOT NULL,
    pass           varchar(50)    NOT NULL,
    CONSTRAINT PK7 PRIMARY KEY NONCLUSTERED (id_empleado)
)
go



IF OBJECT_ID('PCCC_EMPLEADO') IS NOT NULL
    PRINT '<<< CREATED TABLE PCCC_EMPLEADO >>>'
ELSE
    PRINT '<<< FAILED CREATING TABLE PCCC_EMPLEADO >>>'
go

/* 
 * TABLE: PCCC_ESTADO_FACTURA 
 */

CREATE TABLE PCCC_ESTADO_FACTURA(
    id_estado      int            IDENTITY(1,1),
    descripcion    varchar(30)    NOT NULL,
    borrado        char(1)        NOT NULL,
    CONSTRAINT PK22 PRIMARY KEY NONCLUSTERED (id_estado)
)
go



IF OBJECT_ID('PCCC_ESTADO_FACTURA') IS NOT NULL
    PRINT '<<< CREATED TABLE PCCC_ESTADO_FACTURA >>>'
ELSE
    PRINT '<<< FAILED CREATING TABLE PCCC_ESTADO_FACTURA >>>'
go

/* 
 * TABLE: PCCC_ESTADO_FACTURA_DET 
 */

CREATE TABLE PCCC_ESTADO_FACTURA_DET(
    id_estado      int            IDENTITY(1,1),
    descripcion    varchar(30)    NOT NULL,
    borrado        char(1)        NOT NULL,
    CONSTRAINT PK22_1 PRIMARY KEY NONCLUSTERED (id_estado)
)
go



IF OBJECT_ID('PCCC_ESTADO_FACTURA_DET') IS NOT NULL
    PRINT '<<< CREATED TABLE PCCC_ESTADO_FACTURA_DET >>>'
ELSE
    PRINT '<<< FAILED CREATING TABLE PCCC_ESTADO_FACTURA_DET >>>'
go

/* 
 * TABLE: PCCC_ESTADO_FORMA_PAGO 
 */

CREATE TABLE PCCC_ESTADO_FORMA_PAGO(
    id_estado      int            IDENTITY(1,1),
    descripcion    varchar(30)    NOT NULL,
    borrado        char(1)        NOT NULL,
    CONSTRAINT PK22_2 PRIMARY KEY NONCLUSTERED (id_estado)
)
go



IF OBJECT_ID('PCCC_ESTADO_FORMA_PAGO') IS NOT NULL
    PRINT '<<< CREATED TABLE PCCC_ESTADO_FORMA_PAGO >>>'
ELSE
    PRINT '<<< FAILED CREATING TABLE PCCC_ESTADO_FORMA_PAGO >>>'
go

/* 
 * TABLE: PCCC_FACTURA 
 */

CREATE TABLE PCCC_FACTURA(
    id_factura              int         IDENTITY(1,1),
    id_proveedor            int         NULL,
    fecha                   datetime    NULL,
    total_factura           int         NULL,
    id_estado               int         NOT NULL,
    id_condicion_de_pago    int         NOT NULL,
    id_empleado             int         NOT NULL,
    CONSTRAINT PK3 PRIMARY KEY NONCLUSTERED (id_factura)
)
go



IF OBJECT_ID('PCCC_FACTURA') IS NOT NULL
    PRINT '<<< CREATED TABLE PCCC_FACTURA >>>'
ELSE
    PRINT '<<< FAILED CREATING TABLE PCCC_FACTURA >>>'
go

/* 
 * TABLE: PCCC_FACTURA_X_ORDEN_DE_PAGO 
 */

CREATE TABLE PCCC_FACTURA_X_ORDEN_DE_PAGO(
    id_factura          int    NOT NULL,
    id_orden_de_pago    int    NOT NULL,
    CONSTRAINT PK25 PRIMARY KEY NONCLUSTERED (id_factura, id_orden_de_pago)
)
go



IF OBJECT_ID('PCCC_FACTURA_X_ORDEN_DE_PAGO') IS NOT NULL
    PRINT '<<< CREATED TABLE PCCC_FACTURA_X_ORDEN_DE_PAGO >>>'
ELSE
    PRINT '<<< FAILED CREATING TABLE PCCC_FACTURA_X_ORDEN_DE_PAGO >>>'
go

/* 
 * TABLE: PCCC_FORMA_DE_PAGO 
 */

CREATE TABLE PCCC_FORMA_DE_PAGO(
    id_factura              int         NOT NULL,
    id_forma_pago           int         IDENTITY(1,1),
    importe                 int         NULL,
    saldo                   int         NULL,
    fecha_de_pago           datetime    NOT NULL,
    fecha_de_vencimiento    datetime    NOT NULL,
    id_estado               int         NOT NULL,
    CONSTRAINT PK24 PRIMARY KEY NONCLUSTERED (id_factura, id_forma_pago)
)
go



IF OBJECT_ID('PCCC_FORMA_DE_PAGO') IS NOT NULL
    PRINT '<<< CREATED TABLE PCCC_FORMA_DE_PAGO >>>'
ELSE
    PRINT '<<< FAILED CREATING TABLE PCCC_FORMA_DE_PAGO >>>'
go

/* 
 * TABLE: PCCC_LIBRO_BANCO 
 */

CREATE TABLE PCCC_LIBRO_BANCO(
    id_banco           int         NOT NULL,
    id_factura         int         NULL,
    fecha              datetime    NULL,
    debe               int         NULL,
    haber              int         NULL,
    saldo              int         NULL,
    id_nota_credito    int         NULL,
    id_proveedor       int         NOT NULL,
    CONSTRAINT PK13_1 PRIMARY KEY NONCLUSTERED (id_banco)
)
go



IF OBJECT_ID('PCCC_LIBRO_BANCO') IS NOT NULL
    PRINT '<<< CREATED TABLE PCCC_LIBRO_BANCO >>>'
ELSE
    PRINT '<<< FAILED CREATING TABLE PCCC_LIBRO_BANCO >>>'
go

/* 
 * TABLE: PCCC_MATERIA_PRIMA 
 */

CREATE TABLE PCCC_MATERIA_PRIMA(
    id_materia_prima    int            NOT NULL,
    descripcion         varchar(30)    NULL,
    precio              float          NULL,
    estado              varchar(30)    NOT NULL,
    CONSTRAINT PK14_1 PRIMARY KEY NONCLUSTERED (id_materia_prima)
)
go



IF OBJECT_ID('PCCC_MATERIA_PRIMA') IS NOT NULL
    PRINT '<<< CREATED TABLE PCCC_MATERIA_PRIMA >>>'
ELSE
    PRINT '<<< FAILED CREATING TABLE PCCC_MATERIA_PRIMA >>>'
go

/* 
 * TABLE: PCCC_MOV_CTA_CTE_PROVEEDOR 
 */

CREATE TABLE PCCC_MOV_CTA_CTE_PROVEEDOR(
    id_mov_cta_cte_proveedor    int      IDENTITY(1,1),
    id_cta_cte_proveedor        int      NULL,
    SumaResta                   text     NOT NULL,
    monto                       float    NULL,
    tipoDoc                     text     NULL,
    numDoc                      int      NOT NULL,
    cantCuotas                  int      NULL,
    CantCuotasPagadas           int      NULL,
    CONSTRAINT PK30 PRIMARY KEY NONCLUSTERED (id_mov_cta_cte_proveedor)
)
go



IF OBJECT_ID('PCCC_MOV_CTA_CTE_PROVEEDOR') IS NOT NULL
    PRINT '<<< CREATED TABLE PCCC_MOV_CTA_CTE_PROVEEDOR >>>'
ELSE
    PRINT '<<< FAILED CREATING TABLE PCCC_MOV_CTA_CTE_PROVEEDOR >>>'
go

/* 
 * TABLE: PCCC_NOTA_CREDITO 
 */

CREATE TABLE PCCC_NOTA_CREDITO(
    id_nota_credito    int         NOT NULL,
    fecha              datetime    NULL,
    total              int         NOT NULL,
    id_factura         int         NOT NULL,
    CONSTRAINT PK5_1_1 PRIMARY KEY NONCLUSTERED (id_nota_credito)
)
go



IF OBJECT_ID('PCCC_NOTA_CREDITO') IS NOT NULL
    PRINT '<<< CREATED TABLE PCCC_NOTA_CREDITO >>>'
ELSE
    PRINT '<<< FAILED CREATING TABLE PCCC_NOTA_CREDITO >>>'
go

/* 
 * TABLE: PCCC_ORDEN_DE_PAGO 
 */

CREATE TABLE PCCC_ORDEN_DE_PAGO(
    id_orden_de_pago    int         IDENTITY(1,1),
    id_proveedor        int         NOT NULL,
    fecha               datetime    NULL,
    total_orden_pago    int         NULL,
    CONSTRAINT PK8 PRIMARY KEY NONCLUSTERED (id_orden_de_pago)
)
go



IF OBJECT_ID('PCCC_ORDEN_DE_PAGO') IS NOT NULL
    PRINT '<<< CREATED TABLE PCCC_ORDEN_DE_PAGO >>>'
ELSE
    PRINT '<<< FAILED CREATING TABLE PCCC_ORDEN_DE_PAGO >>>'
go

/* 
 * TABLE: PCCC_PERSONA 
 */

CREATE TABLE PCCC_PERSONA(
    num_doc         int            NOT NULL,
    tipo_doc        varchar(20)    NOT NULL,
    nombre          varchar(50)    NULL,
    apellido        varchar(50)    NULL,
    direccion       varchar(50)    NULL,
    email           varchar(50)    NULL,
    nacionalidad    varchar(20)    NULL,
    sexo            char(1)        NULL,
    CONSTRAINT PK8_1 PRIMARY KEY NONCLUSTERED (num_doc, tipo_doc)
)
go



IF OBJECT_ID('PCCC_PERSONA') IS NOT NULL
    PRINT '<<< CREATED TABLE PCCC_PERSONA >>>'
ELSE
    PRINT '<<< FAILED CREATING TABLE PCCC_PERSONA >>>'
go

/* 
 * TABLE: PCCC_PROVEEDORES 
 */

CREATE TABLE PCCC_PROVEEDORES(
    id_proveedor    int            IDENTITY(1,1),
    nombre          varchar(30)    NULL,
    num_doc         int            NULL,
    direccion       varchar(50)    NULL,
    telefono        int            NULL,
    CONSTRAINT PK0 PRIMARY KEY NONCLUSTERED (id_proveedor)
)
go



IF OBJECT_ID('PCCC_PROVEEDORES') IS NOT NULL
    PRINT '<<< CREATED TABLE PCCC_PROVEEDORES >>>'
ELSE
    PRINT '<<< FAILED CREATING TABLE PCCC_PROVEEDORES >>>'
go

/* 
 * TABLE: PCCC_RECIBOS 
 */

CREATE TABLE PCCC_RECIBOS(
    id_recibo       int         IDENTITY(1,1),
    id_proveedor    int         NULL,
    Fecha           datetime    NULL,
    proveedor       int         NOT NULL,
    total           float       NULL,
    CONSTRAINT PK31 PRIMARY KEY NONCLUSTERED (id_recibo)
)
go



IF OBJECT_ID('PCCC_RECIBOS') IS NOT NULL
    PRINT '<<< CREATED TABLE PCCC_RECIBOS >>>'
ELSE
    PRINT '<<< FAILED CREATING TABLE PCCC_RECIBOS >>>'
go

/* 
 * TABLE: PCCC_CHEQUE 
 */

ALTER TABLE PCCC_CHEQUE ADD CONSTRAINT RefPCCC_BANCO42 
    FOREIGN KEY (id_banco)
    REFERENCES PCCC_BANCO(id_banco)
go

ALTER TABLE PCCC_CHEQUE ADD CONSTRAINT RefPCCC_ORDEN_DE_PAGO7 
    FOREIGN KEY (id_orden_de_pago)
    REFERENCES PCCC_ORDEN_DE_PAGO(id_orden_de_pago)
go


/* 
 * TABLE: PCCC_CTA_CTE_PROVEEDOR 
 */

ALTER TABLE PCCC_CTA_CTE_PROVEEDOR ADD CONSTRAINT RefPCCC_PROVEEDORES51 
    FOREIGN KEY (id_proveedor)
    REFERENCES PCCC_PROVEEDORES(id_proveedor)
go


/* 
 * TABLE: PCCC_CUPON_DE_CREDITO 
 */

ALTER TABLE PCCC_CUPON_DE_CREDITO ADD CONSTRAINT RefPCCC_BANCO45 
    FOREIGN KEY (id_banco)
    REFERENCES PCCC_BANCO(id_banco)
go

ALTER TABLE PCCC_CUPON_DE_CREDITO ADD CONSTRAINT RefPCCC_ORDEN_DE_PAGO47 
    FOREIGN KEY (id_orden_de_pago)
    REFERENCES PCCC_ORDEN_DE_PAGO(id_orden_de_pago)
go


/* 
 * TABLE: PCCC_DET_NOTA_CREDITO 
 */

ALTER TABLE PCCC_DET_NOTA_CREDITO ADD CONSTRAINT RefPCCC_NOTA_CREDITO21 
    FOREIGN KEY (id_nota_credito)
    REFERENCES PCCC_NOTA_CREDITO(id_nota_credito)
go


/* 
 * TABLE: PCCC_DETALLE_FACTURA 
 */

ALTER TABLE PCCC_DETALLE_FACTURA ADD CONSTRAINT RefPCCC_FACTURA4 
    FOREIGN KEY (id_factura)
    REFERENCES PCCC_FACTURA(id_factura)
go

ALTER TABLE PCCC_DETALLE_FACTURA ADD CONSTRAINT RefPCCC_MATERIA_PRIMA20 
    FOREIGN KEY (id_materia_prima)
    REFERENCES PCCC_MATERIA_PRIMA(id_materia_prima)
go

ALTER TABLE PCCC_DETALLE_FACTURA ADD CONSTRAINT RefPCCC_ESTADO_FACTURA_DET33 
    FOREIGN KEY (id_estado)
    REFERENCES PCCC_ESTADO_FACTURA_DET(id_estado)
go


/* 
 * TABLE: PCCC_DETALLE_RECIBO 
 */

ALTER TABLE PCCC_DETALLE_RECIBO ADD CONSTRAINT RefPCCC_RECIBOS52 
    FOREIGN KEY (id_recibo)
    REFERENCES PCCC_RECIBOS(id_recibo)
go


/* 
 * TABLE: PCCC_EMPLEADO 
 */

ALTER TABLE PCCC_EMPLEADO ADD CONSTRAINT RefPCCC_PERSONA1 
    FOREIGN KEY (num_doc, tipo_doc)
    REFERENCES PCCC_PERSONA(num_doc, tipo_doc)
go


/* 
 * TABLE: PCCC_FACTURA 
 */

ALTER TABLE PCCC_FACTURA ADD CONSTRAINT RefPCCC_PROVEEDORES6 
    FOREIGN KEY (id_proveedor)
    REFERENCES PCCC_PROVEEDORES(id_proveedor)
go

ALTER TABLE PCCC_FACTURA ADD CONSTRAINT RefPCCC_ESTADO_FACTURA32 
    FOREIGN KEY (id_estado)
    REFERENCES PCCC_ESTADO_FACTURA(id_estado)
go

ALTER TABLE PCCC_FACTURA ADD CONSTRAINT RefPCCC_CONDICION_DE_PAGO37 
    FOREIGN KEY (id_condicion_de_pago)
    REFERENCES PCCC_CONDICION_DE_PAGO(id_condicion_de_pago)
go

ALTER TABLE PCCC_FACTURA ADD CONSTRAINT RefPCCC_EMPLEADO38 
    FOREIGN KEY (id_empleado)
    REFERENCES PCCC_EMPLEADO(id_empleado)
go


/* 
 * TABLE: PCCC_FACTURA_X_ORDEN_DE_PAGO 
 */

ALTER TABLE PCCC_FACTURA_X_ORDEN_DE_PAGO ADD CONSTRAINT RefPCCC_FACTURA41 
    FOREIGN KEY (id_factura)
    REFERENCES PCCC_FACTURA(id_factura)
go

ALTER TABLE PCCC_FACTURA_X_ORDEN_DE_PAGO ADD CONSTRAINT RefPCCC_ORDEN_DE_PAGO40 
    FOREIGN KEY (id_orden_de_pago)
    REFERENCES PCCC_ORDEN_DE_PAGO(id_orden_de_pago)
go


/* 
 * TABLE: PCCC_FORMA_DE_PAGO 
 */

ALTER TABLE PCCC_FORMA_DE_PAGO ADD CONSTRAINT RefPCCC_ESTADO_FORMA_PAGO49 
    FOREIGN KEY (id_estado)
    REFERENCES PCCC_ESTADO_FORMA_PAGO(id_estado)
go

ALTER TABLE PCCC_FORMA_DE_PAGO ADD CONSTRAINT RefPCCC_FACTURA35 
    FOREIGN KEY (id_factura)
    REFERENCES PCCC_FACTURA(id_factura)
go


/* 
 * TABLE: PCCC_LIBRO_BANCO 
 */

ALTER TABLE PCCC_LIBRO_BANCO ADD CONSTRAINT RefPCCC_BANCO43 
    FOREIGN KEY (id_banco)
    REFERENCES PCCC_BANCO(id_banco)
go

ALTER TABLE PCCC_LIBRO_BANCO ADD CONSTRAINT RefPCCC_PROVEEDORES44 
    FOREIGN KEY (id_proveedor)
    REFERENCES PCCC_PROVEEDORES(id_proveedor)
go

ALTER TABLE PCCC_LIBRO_BANCO ADD CONSTRAINT RefPCCC_FACTURA23 
    FOREIGN KEY (id_factura)
    REFERENCES PCCC_FACTURA(id_factura)
go

ALTER TABLE PCCC_LIBRO_BANCO ADD CONSTRAINT RefPCCC_NOTA_CREDITO31 
    FOREIGN KEY (id_nota_credito)
    REFERENCES PCCC_NOTA_CREDITO(id_nota_credito)
go


/* 
 * TABLE: PCCC_MOV_CTA_CTE_PROVEEDOR 
 */

ALTER TABLE PCCC_MOV_CTA_CTE_PROVEEDOR ADD CONSTRAINT RefPCCC_CTA_CTE_PROVEEDOR50 
    FOREIGN KEY (id_cta_cte_proveedor)
    REFERENCES PCCC_CTA_CTE_PROVEEDOR(id_cta_cte_proveedor)
go


/* 
 * TABLE: PCCC_NOTA_CREDITO 
 */

ALTER TABLE PCCC_NOTA_CREDITO ADD CONSTRAINT RefPCCC_FACTURA22 
    FOREIGN KEY (id_factura)
    REFERENCES PCCC_FACTURA(id_factura)
go


/* 
 * TABLE: PCCC_ORDEN_DE_PAGO 
 */

ALTER TABLE PCCC_ORDEN_DE_PAGO ADD CONSTRAINT RefPCCC_PROVEEDORES13 
    FOREIGN KEY (id_proveedor)
    REFERENCES PCCC_PROVEEDORES(id_proveedor)
go


/* 
 * TABLE: PCCC_RECIBOS 
 */

ALTER TABLE PCCC_RECIBOS ADD CONSTRAINT RefPCCC_PROVEEDORES53 
    FOREIGN KEY (id_proveedor)
    REFERENCES PCCC_PROVEEDORES(id_proveedor)
go


