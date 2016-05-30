/*
    This file is part of SmartApp.

    SmartApp is free software: you can redistribute it and/or modify
    it under the terms of the GNU General Public License as published by
    the Free Software Foundation, either version 3 of the License, or
    (at your option) any later version.

    SmartApp is distributed in the hope that it will be useful,
    but WITHOUT ANY WARRANTY; without even the implied warranty of
    MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
    GNU General Public License for more details.

    You should have received a copy of the GNU General Public License
    along with Foobar.  If not, see <http://www.gnu.org/licenses/>.
*/
using System;
using System.Collections.Generic;
using System.Text;
#if !PocketPC
using System.Reflection.Emit;
#endif

namespace CommonLib
{
    public class ILInstruction
    {

        // Fields
#if !PocketPC
        private OpCode code;
#endif
        private object operand;
        private byte[] operandData;
        private int offset;

        // Properties
#if !PocketPC

        public OpCode Code
        {
            get { return code; }
            set { code = value; }
        }
#endif
        public object Operand
        {
            get { return operand; }
            set { operand = value; }
        }

        public byte[] OperandData
        {
            get { return operandData; }
            set { operandData = value; }
        }

        public int Offset
        {
            get { return offset; }
            set { offset = value; }
        }

        /// <summary>
        /// Returns a friendly strign representation of this instruction
        /// </summary>
        /// <returns></returns>
        public string GetCode()
        {
#if !PocketPC
            string result = "";
            result += GetExpandedOffset(offset) + " : " + code;
            if (operand != null)
            {
                switch (code.OperandType)
                {
                    case OperandType.InlineField:
                        System.Reflection.FieldInfo fOperand = ((System.Reflection.FieldInfo)operand);
                        result += " " + Globals.ProcessSpecialTypes(fOperand.FieldType.ToString()) + " " +
                            Globals.ProcessSpecialTypes(fOperand.ReflectedType.ToString()) +
                            "::" + fOperand.Name + "";
                        break;
                    case OperandType.InlineMethod:
                        System.Reflection.MethodInfo mOperand = operand as System.Reflection.MethodInfo;
                        if (mOperand != null)
                        {
                            result += " ";
                            if (!mOperand.IsStatic) result += "instance ";
                            result += Globals.ProcessSpecialTypes(mOperand.ReturnType.ToString()) +
                                " " + Globals.ProcessSpecialTypes(mOperand.ReflectedType.ToString()) +
                                "::" + mOperand.Name + "()";
                        }
                        else
                        {
                            System.Reflection.ConstructorInfo mOperand2 = operand as System.Reflection.ConstructorInfo;
                            if (mOperand2 != null)
                            {
                                result += " ";
                                if (!mOperand2.IsStatic) result += "instance ";
                                result += "void " +
                                    Globals.ProcessSpecialTypes(mOperand2.ReflectedType.ToString()) +
                                    "::" + mOperand2.Name + "()";
                            }
                        }
                        break;
                    case OperandType.ShortInlineBrTarget:
                    case OperandType.InlineBrTarget:
                        result += " " + GetExpandedOffset((int)operand);
                        break;
                    case OperandType.InlineType:
                        result += " " + Globals.ProcessSpecialTypes(operand.ToString());
                        break;
                    case OperandType.InlineString:
                        if (operand.ToString() == "\r\n") result += " \"\\r\\n\"";
                        else result += " \"" + operand.ToString() + "\"";
                        break;
                    case OperandType.ShortInlineVar:
                        result += operand.ToString();
                        break;
                    case OperandType.InlineI:
                    case OperandType.InlineI8:
                    case OperandType.InlineR:
                    case OperandType.ShortInlineI:
                    case OperandType.ShortInlineR:
                        result += operand.ToString();
                        break;
                    case OperandType.InlineTok:
                        if (operand is Type)
                            result += ((Type)operand).FullName;
                        else
                            result += "not supported";
                        break;

                    default: result += "not supported"; break;
                }
            }
            return result;
#else
            return "";
#endif
        }

        /// <summary>
        /// Add enough zeros to a number as to be represented on 4 characters
        /// </summary>
        /// <param name="offset">
        /// The number that must be represented on 4 characters
        /// </param>
        /// <returns>
        /// </returns>
        private string GetExpandedOffset(long offset)
        {
            string result = offset.ToString();
            for (int i = 0; result.Length < 4; i++)
            {
                result = "0" + result;
            }
            return result;
        }

        public ILInstruction()
        {

        }
    }
}
