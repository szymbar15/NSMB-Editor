﻿using System;
using System.Collections.Generic;
using System.Text;

namespace NSMBe4.DSFileSystem
{
    public class InlineFile : File
    {
        private int inlineOffs;
        private int inlineLen;
        private File parentFile;

        public InlineFile(File parent, int offs, int len, string name, Directory parentDir)
            :base(parent.parent, parentDir, parent.name+" - "+name)
        {
            parentFile = parent;
            inlineOffs = offs;
            inlineLen = len;
            this.fixedFile = true;
            this.canChangeOffset = false;
            refreshOffsets();
        }

        public override void beginEdit(object editor)
        {
            parentFile.beginEditInline(this);
            base.beginEdit(editor);
        }

        public override void endEdit(object editor)
        {
            parentFile.endEditInline(this);
            base.endEdit(editor);
        }

        public override void refreshOffsets()
        {
            fileBegin = parentFile.fileBegin + inlineLen;
            fileSize = inlineLen;
        }

        public override void saveOffsets()
        {
        }
    }
}
